from pyspark.sql import SparkSession
from pyspark.sql.functions import from_json

# Define Avro schema
avro_schema = """
{
  "type": "tweet",
  "name": "Tweet",
  "fields": [
    { "name": "date", "type": "int" },
    { "name": "name", "type": "string" },
    { "name": "name", "type": "string" }
  ]
}
"""

if __name__ == "__main__":
    spark = SparkSession\
        .builder\
        .appName("TweetSentiment")\
        .getOrCreate()
    
    # Read data from Kafka topic in JSON format
    json_df = spark.readStream\
        .format("kafka")\
        .option("kafka.bootstrap.servers", "192.168.1.100:9092")\
        .option("subscribe", "tweet-json")\
        .option("startingOffsets", "earliest")\
        .load()\
        .select(from_json(col("value").cast("string"), schema=avro_schema).alias("data"))
    
    # Do something here
    
    
    # Write data to Kafka topic in Avro format
    #avro_df.selectExpr("CAST(id AS STRING) AS key", "to_json(struct(*)) AS value")\
    avro_df.writeStream\
        .format("kafka")\
        .outputMode("append")\
        .option("kafka.bootstrap.servers", "192.168.1.100:9092")\
        .option("topic", "avro_topic")\
        .option("valueFormat", "int")\
        .start()\
        .awaitTermination()

    #partitions = int(sys.argv[1]) if len(sys.argv) > 1 else 2
    #n = 100000 * partitions

    #def f(_: int) -> float:
    #    x = random() * 2 - 1
    #    y = random() * 2 - 1
    #    return 1 if x ** 2 + y ** 2 <= 1 else 0

    #count = spark.sparkContext.parallelize(range(1, n + 1), partitions).map(f).reduce(add)
    #print("Pi is roughly %f" % (4.0 * count / n))

    #spark.stop()