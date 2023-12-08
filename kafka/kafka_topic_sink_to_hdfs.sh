#!/bin/sh
curl -X POST \
http://127.0.0.1:8083/connectors \
-H 'Content-Type: application/json' \
-d '{
    "name": "hdfs-sink-'"$1"'",
    "config": {
        "connector.class": "io.confluent.connect.hdfs.HdfsSinkConnector",
        "tasks.max": "'"$2"'",
        "topics": "'"$1"'",
        "hdfs.url": "hdfs://hdfs-namenode-default-0.stackable:8020",
        "flush.size": "'"$3"'",
        "format.class": "io.confluent.connect.hdfs.json.JsonFormat",
        "key.converter.schemas.enable":"false",
        "key.converter": "org.apache.kafka.connect.storage.StringConverter",
        "key.converter.schema.registry.url": "http://kafka-schema-registry.kafka:8081", 
        "value.converter.schemas.enable":"false",
        "value.converter.schema.registry.url": "http://kafka-schema-registry.kafka:8081", 
        "value.converter": "org.apache.kafka.connect.json.JsonConverter"
    }
}'