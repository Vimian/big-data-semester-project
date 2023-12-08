ECHO off
cls

ECHO Uninstall HDFS
ECHO.

cd hdfs

kubectl delete -f hdfs-cli.yaml -n stackable

kubectl delete -f zk.yaml -n stackable

kubectl delete -f znode.yaml -n stackable

kubectl delete -f hdfs.yaml -n stackable

helm uninstall -n stackable zookeeper-operator
helm uninstall -n stackable hdfs-operator
helm uninstall -n stackable commons-operator
helm uninstall -n stackable secret-operator

cd ..

ECHO Uninstall Spark
ECHO.

cd spark

kubectl delete -f spark-configurations.yaml -n stackable

kubectl delete -f spark-history-server.yaml -n stackable

helm uninstall -n stackable commons-operator
helm uninstall -n stackable secret-operator
helm uninstall -n stackable spark-k8s-operator

helm uninstall -n stackable minio

cd ..

ECHO Uninstall Kafka
ECHO.

cd kafka

kubectl delete -f kafka.yaml -n kafka 

kubectl delete -f kafka-connect.yaml -n kafka

kubectl -n kafka delete -f "https://strimzi.io/install/latest?namespace=kafka"

kubectl delete -f redpanda.yaml -n kafka

cd ..

ECHO Uninstall Redis
ECHO.

cd redis

kubectl delete -f redis.yaml -n redis
kubectl delete -f redisinsight.yaml -n redis
helm uninstall redis-operator -n redis

cd ..

ECHO Uninstall dataset loader

kubectl delete -f ./mockup/dataset-loader/dataset-loader.yaml -n mockup

ECHO.
ECHO Uninstall successful
pause