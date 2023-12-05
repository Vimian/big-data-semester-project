const { Kafka } = require("kafkajs");

const kafka = new Kafka({
    clientId: "mockup-dataset-loader",
    brokers: ["kafka-cluster-kafka-bootstrap.kafka:9092"],
});

const producer = kafka.producer({
    allowAutoTopicCreation: true,
});

const run = async () => {
    console.log("Connecting to Kafka1");
    await producer.connect();
    console.log("Connected to Kafka1");
    await producer.send({
        topic: "gold-json",
        messages: [
            { value: "Hello KafkaJS user!" },
            { value: "Hello KafkaJS user2!" },
        ],
    });
};

run().catch(console.error);
