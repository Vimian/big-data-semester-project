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
        .config("spark.streaming.stopGracefullyOnShutdown", "true")\
        .config("spark.jars.packages", "org.apache.spark:spark-sql-kafka-0-10_2.12:3.3.0")\
        .config("spark.sql.shuffle.partitions", 2)\
        .master("local[*]")\
        .getOrCreate()
    print("000000000000000000000")
    # Read data from Kafka topic in JSON format
    json_df = spark.readStream\
        .format("kafka")\
        .option("kafka.bootstrap.servers", "kafka-cluster-kafka-bootstrap.kafka:9092")\
        .option("subscribe", "tweet-json")\
        .option("startingOffsets", "earliest")\
        .load()
    print("11111111111111111111")
    # Do something here
    redis = redis.Redis(host='localhost', port=6379, db=0)
    print("22222222222222222222")
    date = json_df.select(
        col("data.date")
    )
    print(date)

    sentiment = json_df.select(
        col("data.sentiment")
    )
    print(sentiment)
    new_sentiment = {
        "positive": null,
        "negative": null
    }
    print(new_sentiment)
    print("33333333333333333333333333333333")
    if sentiment == "Positive":
        new_sentiment["positive"] = redis.incr(date + ":positive")
    else:
        new_sentiment["negative"] = redis.incr(date + ":negative")
    print("4444444444444444444444444444444")
    
    # Write data to Kafka topic in Avro format
    #avro_df.selectExpr("CAST(id AS STRING) AS key", "to_json(struct(*)) AS value")\
    json_df.writeStream\
        .format("kafka")\
        .outputMode("append")\
        .option("kafka.bootstrap.servers", "192.168.1.100:9092")\
        .option("topic", "json_sentiment")\
        .option("valueFormat", "json")\
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