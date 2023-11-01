using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

class Server {
    public static void Main(string[] args) {
        string ip = "127.0.0.1";
        int port = 80;
        var server = new TcpListener(IPAddress.Parse(ip), port);

        server.Start();
        Console.WriteLine($"Server has started, waiting for connection, IP: {ip}, Port: {port}");

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Client connected, {client.Connected}");
    }
}