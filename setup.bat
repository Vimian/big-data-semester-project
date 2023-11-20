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

ECHO.
ECHO Setup successful
pause