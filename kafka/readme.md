# create kafka cluster

```
kubectl create namespace kafka
```

```
kubectl create -f "https://strimzi.io/install/latest?namespace=kafka" -n kafka
```

```
kubectl apply -f kafka.yaml -n kafka
```

# create topics

```
kubectl apply -f ./topics/gold-json.yaml -n kafka
kubectl apply -f ./topics/gold-avro.yaml -n kafka
kubectl apply -f ./topics/stock-json.yaml -n kafka
kubectl apply -f ./topics/stock-avro.yaml -n kafka
kubectl apply -f ./topics/tweet-json.yaml -n kafka
kubectl apply -f ./topics/tweet-avro.yaml -n kafka
```

```
kubectl apply -f ./topics/result-paquet.yaml -n kafka
```

# build kafka connect base image

```
docker build -t cp-server-connect-base -f ./Dockerfile .
```

# create kafka connect cluster

```
kubectl apply -f kafka-connect.yaml -n kafka
```

# debugger install

```
kubectl apply -f redpanda.yaml -n kafka
```

```
kubectl port-forward svc/redpanda  8080:8080 -n kafka
```

# clean up

```
kubectl delete -f kafka.yaml -n kafka

kubectl delete -f kafka-connect.yaml -n kafka

kubectl -n kafka delete -f "https://strimzi.io/install/latest?namespace=kafka"

kubectl delete -f redpanda.yaml -n kafka

kubectl delete namespace kafka
```
