from pyspark.sql import SparkSession
from pyspark.sql.functions import *
from pyspark.sql.types import *
import pyhdfs
from json import *
import time

# Define schema of JSON data
schema = StructType()\
    .add("Date", StringType())\
    .add("text", StringType())\
    .add("Sentiment", StringType())

if __name__ == "__main__":
    # Create Spark session
    spark = SparkSession\
        .builder\
        .appName("TweetSentiment")\
        .getOrCreate()
    
    # Read from Kafka topic in JSON format and load into Spark dataframe
    json_df = spark.readStream\
        .format("kafka")\
        .option("kafka.bootstrap.servers", "kafka-cluster-kafka-bootstrap.kafka:9092")\
        .option("subscribe", "tweet-json")\
        .option("startingOffsets", "earliest")\
        .option("kafka.group.id", "sink-gold")\
        .option("maxOffsetsPerTrigger", "1")\
        .load()\
        .select(from_json(col("value").cast("string"), schema).alias("data"))\
        .select("data.*")
        # limit the number of records per trigger to 1
    
    
    #def sink_to_hdfs(row):
        # Create HDFS connection
    #    client = InsecureClient('http://hdfs-namenode-default-0.hdfs-namenode-default.stackable:9870', user="stackable")

        #print(f"Writing row to HDFS: {row}")

        # Write to HDFS
        #with client.write(f"/gold/{row['Date']}", append=True) as writer:
        #    dump({"text": row['text'], "Sentiment": row['Sentiment']}, writer)
        #client.write(f"/gold/{row['Date']}", data=dumps({"text": row['text'], "Sentiment": row['Sentiment']}), encoding='utf-8', overwrite=False, append=True)

    #    return

    def upload_to_hdfs(row):
        print(row['Date'])
        # Create HDFS connection
        #client = InsecureClient("hdfs://hdfs-namenode-default-0.hdfs-namenode-default.stackable:8020", user="stackable")
        # Write to HDFS
        #with client.write(f"/gold/{row['Date']}", append=True) as writer:
        #    result = writer.write("text")

        #print(result)
        fs = pyhdfs.HdfsClient(hosts="hdfs-namenode-default-0.hdfs-namenode-default.stackable:9870", user_name="stackable")
        print("22222222222222222222222")
        fs.append(f"/gold/{row['Date']}", dumps({"text": row['text'], "Sentiment": row['Sentiment']}) + "\n")
        time.sleep(5)
        #print(client.list("/gold/", status=True))
        print("33333333333333333333333")

    def sink_to_hdfs(batch_df, batch_id):
        print("11111111111111111111111")

        # print length of batch
        print(f"Batch {batch_id} has {batch_df.count()} records")

        for row in batch_df.collect():
            print("555555555555555555555")
            upload_to_hdfs(row)
            

    query = json_df.writeStream\
        .outputMode("append")\
        .format("console")\
        .foreachBatch(sink_to_hdfs)\
        .trigger(once=True)\
        .start()

    query.awaitTermination()