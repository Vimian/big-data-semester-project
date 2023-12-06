from pyspark.sql import SparkSession
from pyspark.sql.functions import *
from pyspark.sql.types import *
import redis

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
        .option("kafka.group.id", "tweet-sentiment")\
        .load()\
        .select(from_json(col("value").cast("string"), schema).alias("data"))\
        .select("data.*")
        # limit the number of records per trigger to 1
        #.option("maxOffsetsPerTrigger", "1")\
    
    # Define a user-defined function (UDF) to print the values
    def update_redis_udf(row):
        # Create Redis connection
        redis_connection = redis.Redis(host="redis-cluster-leader.redis", port=6379, decode_responses=True, db=0)
        redis_connection.ping()

        # Key is <the date>:<the sentiment>
        key = f"{row['Date']}:{row['Sentiment']}"

        # Increment the value of the key if fails do it again
        try:
            redis_connection.incrby(key, 1)
        except redis.exceptions.ResponseError as e:
            #print(f"Error updating {key}")
            update_redis_udf(row)
        return

    query = json_df.writeStream\
        .outputMode("append")\
        .format("console")\
        .foreach(update_redis_udf)\
        .start()

    query.awaitTermination()