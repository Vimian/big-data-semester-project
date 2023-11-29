const { Kafka } = require("kafkajs");

const kafka = new Kafka({
    clientId: "mockup-dataset-loader",
    brokers: ["kafka-cluster-kafka-bootstrap:9092"],
});

const producer = kafka.producer();

const run = async () => {
    await producer.connect();
    await producer.send({
        topic: "gold-json",
        messages: [
            { value: "Hello KafkaJS user!" },
            { value: "Hello KafkaJS user2!" },
        ],
    });
};

run().catch(console.error);
