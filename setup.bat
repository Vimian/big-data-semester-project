ECHO off
cls

ECHO Setup HDFS
ECHO.

cd hdfs

ECHO create operators
ECHO.

helm repo add stackable-stable https://repo.stackable.tech/repository/helm-stable/
helm repo update

kubectl create namespace stackable

helm install -n stackable zookeeper-operator stackable-stable/zookeeper-operator --version 23.7.0
helm install -n stackable hdfs-operator stackable-stable/hdfs-operator --version 23.7.0
helm install -n stackable commons-operator stackable-stable/commons-operator --version 23.7.0
helm install -n stackable secret-operator stackable-stable/secret-operator --version 23.7.0

ECHO create hdfs cluster
ECHO.

kubectl apply -f zk.yaml -n stackable

kubectl apply -f znode.yaml -n stackable

kubectl apply -f hdfs.yaml -n stackable

cd ..

ECHO Setup Spark
ECHO.

cd spark

ECHO create operators

helm repo add stackable-stable https://repo.stackable.tech/repository/helm-stable/
helm repo update

kubectl create namespace stackable

helm install -n stackable commons-operator stackable-stable/commons-operator --version 23.7.0
helm install -n stackable secret-operator stackable-stable/secret-operator --version 23.7.0
helm install -n stackable spark-k8s-operator stackable-stable/spark-k8s-operator --version 23.7.0

ECHO create minio

helm install -n stackable minio oci://registry-1.docker.io/bitnamicharts/minio --set service.type=NodePort --set defaultBuckets="spark-logs/eventlogs;spark-jobs" --set auth.rootUser=admin --set auth.rootPassword=password --set tls.autoGenerated=true

ECHO create spark configurations

kubectl apply -f spark-configurations.yaml -n stackable

cd ..

ECHO Setup Kafka
ECHO.

cd kafka

ECHO create operators
ECHO.

kubectl create namespace kafka

kubectl create -f "https://strimzi.io/install/latest?namespace=kafka" -n kafka

kubectl apply -f kafka.yaml -n kafka

ECHO create kafka topics

kubectl apply -f ./topics/gold-json.yaml -n kafka
kubectl apply -f ./topics/gold-avro.yaml -n kafka

kubectl apply -f ./topics/stock-json.yaml -n kafka
kubectl apply -f ./topics/stock-avro.yaml -n kafka

kubectl apply -f ./topics/tweet-json.yaml -n kafka
kubectl apply -f ./topics/tweet-avro.yaml -n kafka

kubectl apply -f ./topics/result-paquet.yaml -n kafka

ECHO create kafka cluster

kubectl apply -f kafka-connect.yaml -n kafka

cd ..

ECHO Setup redis
ECHO.

cd redis

helm repo add ot-helm https://ot-container-kit.github.io/helm-charts/
helm repo update

kubectl create namespace redis

helm install redis-operator ot-helm/redis-operator -n redis

kubectl apply -f redis.yaml -n redis

cd ..

ECHO.
ECHO Setup successful
pause