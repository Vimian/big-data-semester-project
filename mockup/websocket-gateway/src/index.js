const { Kafka } = require("kafkajs");
const ws = require("ws");

// Create websocket server
const wss = new ws.WebSocketServer({ port: 8080 });

wss.on("connection", (ws) => {
    ws.on("error", (error) => {
        console.log(`Received error => ${error}`);
    });

    ws.on("message", (message) => {
        console.log(`Received message => ${message}`);
    });
});

// Create kafka consumer
const kafka = new Kafka({
    clientId: "mockup-dataset-loader",
    brokers: ["kafka-cluster-kafka-bootstrap.kafka:9092"],
});

const consumer = kafka.consumer({ groupId: "mockup-dataset-loader" });

const run = async () => {
    await consumer.connect();
    await consumer.subscribe({
        topics: ["gold-json", "stock-json", "tweet-json"],
        fromBeginning: false,
    });

    await consumer.run({
        eachMessage: async ({ topic, partition, message }) => {
            wss.clients.forEach((client) => {
                if (client.readyState === ws.WebSocket.OPEN) {
                    if (topic === "tweet-json") {
                        client.send(
                            JSON.stringify({
                                topic: topic,
                                message: message.value.toString(),
                            })
                        );
                    } else if (topic === "gold-json") {
                        let messageJSON = JSON.parse(message.value.toString());
                        let newMessage = {
                            timestamp: messageJSON.timestamp,
                            xauusd_close: messageJSON.xauusd_close,
                        };
                        client.send(
                            JSON.stringify({
                                topic: topic,
                                message: newMessage,
                            })
                        );
                    } else if (topic === "stock-json") {
                        let messageJSON = JSON.parse(message.value.toString());
                        let newMessage = {
                            date: messageJSON.date,
                            close: messageJSON.close,
                        };
                        client.send(
                            JSON.stringify({
                                topic: topic,
                                message: newMessage,
                            })
                        );
                    }
                }
            });
        },
    });
};

run().catch(console.error);
