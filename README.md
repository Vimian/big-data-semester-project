# big-data-semester-project

## Debuggers

Redpanda

```
kubectl port-forward svc/redpanda  8080:8080 -n kafka
```

Minio webui

```
kubectl port-forward -n stackable svc/minio 9001:9001
```

Spark history webui

```
kubectl port-forward -n stackable svc/spark-history-node 18080:18080
```

Redis Insight

```
kubectl port-forward svc/redisinsight-service  8001:8001 -n redis
```

## Twitter API:

```
/
```

Returns 200, use this to confirm that everything is working as intended

From here you can

```
/GetLatestTweets/{number}
```

Gives latest tweets within the number set

```
/GetFirst/{number}
```

Mostly for debuggin purposes, gets tweets from the start of the csv file

```
/GetBetween+{number0}&{number1}
```

Most likely the development api path, gives between the 2 numbers

Note: The csv file is required to be at this location

```
...\GitHub\big-data-semester-project\TwitterBitcoinAPI\
```

## StockWebsocket

IP: Which is set, don't change
PORT: which is set, don't change
PathToCSV: Which is not set and you would have to change that

Be aware, IP and PORT can be changed if you're working outside of the docker containers

```
dotnet StockMarketWebSocket.dll THE_IP THE_PORT
```

If you run it outside of the docker and wants to test it, edit the client.html so that the IP is the same as THE_IP, keep the 'ws://' part
