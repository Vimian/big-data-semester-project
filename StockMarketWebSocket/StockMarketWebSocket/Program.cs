using StockMarketWebSocket.CSV;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

public static class XLExtensions {
    public static IEnumerable<string> SplitInGroups(this string original, int size) {
        var p = 0;
        var l = original.Length;
        while(l - p > size) {
            yield return original.Substring(p, size);
            p += size;
        }
        yield return original.Substring(p);
    }
}

class Server {
    //Stole some code from here
    //https://stackoverflow.com/questions/27021665/c-sharp-websocket-sending-message-back-to-client
    public static string path = "D:/StockmarketCSV/";
    public static void Main(string[] args) {
        string ip = "127.0.0.1";
        int port = 80;
        if(args.Length != 0) {
            ip = args[0];
            port = int.Parse(args[1]);
            path = args[2];
            if(args[2] == "actions") {
                return;
            }
        }


        var server = new TcpListener(IPAddress.Parse(ip), port);

        server.Start();
        Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection�", ip, port);
        StockController stockController = new StockController(true);
        while(true) {

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();
            int j = 0;

            while(true) {

                while(!stream.DataAvailable) ;
                while(client.Available < 3) ; // match against "get"

                byte[] bytes = new byte[client.Available];
                stream.Read(bytes, 0, bytes.Length);
                string s = Encoding.UTF8.GetString(bytes);
                //Console.WriteLine(Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase));
                if(Regex.IsMatch(s, "^GET", RegexOptions.IgnoreCase)) {
                    Console.WriteLine("=====Handshaking from client=====\n{0}", s);

                    // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
                    // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
                    // 3. Compute SHA-1 and Base64 hash of the new value
                    // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
                    string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                    string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                    string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                    // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                    byte[] response = Encoding.UTF8.GetBytes(
                        "HTTP/1.1 101 Switching Protocols\r\n" +
                        "Connection: Upgrade\r\n" +
                        "Upgrade: websocket\r\n" +
                        "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                    stream.Write(response, 0, response.Length);
                    string msg = "Hello World";

                    Queue<string> que = new Queue<string>(msg.SplitInGroups(125));
                    int len = que.Count;

                    int GetHeader(bool finalFrame, bool contFrame) {
                        int header = finalFrame ? 1 : 0;//fin: 0 = more frames, 1 = final frame
                        header = (header << 1) + 0;//rsv1
                        header = (header << 1) + 0;//rsv2
                        header = (header << 1) + 0;//rsv3
                        header = (header << 4) + (contFrame ? 0 : 1);//opcode : 0 = continuation frame, 1 = text
                        header = (header << 1) + 0;//mask: server -> client = no mask
                        return header;
                    }

                    while(que.Count > 0) {
                        var header = GetHeader(
                            que.Count > 1 ? false : true,
                            que.Count == len ? false : true
                        );

                        byte[] list = Encoding.UTF8.GetBytes(que.Dequeue());
                        header = (header << 7) + list.Length;
                        var ary = BitConverter.GetBytes((ushort)header);
                        if(BitConverter.IsLittleEndian) {
                            Array.Reverse(ary);
                        }
                        stream.Write(ary, 0, 2);
                        stream.Write(list, 0, list.Length);
                        Console.WriteLine(que.Count);
                    }
                    int k = 0;
                    while (k < 50) {
                        Thread.Sleep(10);
                        SendEcho(stream, stockController.getStockString(k));
                        k++;
                        if (k == 50) k = 0;
                    }


                }
                else {
                    bool fin = (bytes[0] & 0b10000000) != 0,
                        mask = (bytes[1] & 0b10000000) != 0; // must be true, "All messages from the client to the server have this bit set"
                    int opcode = bytes[0] & 0b00001111, // expecting 1 - text message
                        offset = 2;
                    ulong msglen = (ulong)(bytes[1] & 0b01111111);

                    if(msglen == 126) {
                        // bytes are reversed because websocket will print them in Big-Endian, whereas
                        // BitConverter will want them arranged in little-endian on windows
                        msglen = BitConverter.ToUInt16(new byte[] { bytes[3], bytes[2] }, 0);
                        offset = 4;
                    }
                    else if(msglen == 127) {
                        // To test the below code, we need to manually buffer larger messages � since the NIC's autobuffering
                        // may be too latency-friendly for this code to run (that is, we may have only some of the bytes in this
                        // websocket frame available through client.Available).
                        msglen = BitConverter.ToUInt64(new byte[] { bytes[9], bytes[8], bytes[7], bytes[6], bytes[5], bytes[4], bytes[3], bytes[2] }, 0);
                        offset = 10;
                    }

                    if(msglen == 0) {
                        Console.WriteLine("msglen == 0");
                    }
                    else if(mask) {
                        byte[] decoded = new byte[msglen];
                        byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                        offset += 4;

                        for(ulong i = 0; i < msglen; ++i)
                            decoded[i] = (byte)(bytes[offset + (int)i] ^ masks[i % 4]);

                        string text = Encoding.UTF8.GetString(decoded);
                        Console.WriteLine("{0}", text);
                    }
                    else
                        Console.WriteLine("mask bit not set");
                    Console.WriteLine();
                }
            }
        }
        //Thank the fine gentleman in here for the code: https://stackoverflow.com/questions/71055243/how-to-send-message-to-client-in-socket-stream
        static void SendEcho(NetworkStream stream, string inputText) {

            byte[] sendBytes = Encoding.UTF8.GetBytes(inputText);
            byte lengthHeader = 0;
            byte[] lengthCount = new byte[] { };

            if(sendBytes.Length <= 125)
                lengthHeader = (byte)sendBytes.Length;

            if(125 < sendBytes.Length && sendBytes.Length < 65535) //System.UInt16
            {
                lengthHeader = 126;

                lengthCount = new byte[] {
                (byte)(sendBytes.Length >> 8),
                (byte)(sendBytes.Length)
            };
            }

            if(sendBytes.Length > 65535)//max 2_147_483_647 but .Length -> System.Int32
            {
                lengthHeader = 127;
                lengthCount = new byte[] {
                (byte)(sendBytes.Length >> 56),
                (byte)(sendBytes.Length >> 48),
                (byte)(sendBytes.Length >> 40),
                (byte)(sendBytes.Length >> 32),
                (byte)(sendBytes.Length >> 24),
                (byte)(sendBytes.Length >> 16),
                (byte)(sendBytes.Length >> 8),
                (byte)sendBytes.Length,
            };
            }

            List<byte> responseArray = new List<byte>() { 0b10000001 };

            responseArray.Add(lengthHeader);
            responseArray.AddRange(lengthCount);
            responseArray.AddRange(sendBytes);

            stream.Write(responseArray.ToArray(), 0, responseArray.Count);
        }
    }
}
