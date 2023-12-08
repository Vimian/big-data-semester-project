ECHO off
cls

ECHO Build docker image
ECHO.
docker image build --tag twitter-connector .

ECHO Run docker image
ECHO.
docker run twitter-connector


ECHO Create Namespace Kafka
ECHO.
kubectl create namespace kafka

cd ..
ECHO Setup Kafka Cluster
ECHO.
kubectl apply -f kafka-connect.yaml -n kafka
cd connect

ECHO Setup Connect Configurations
ECHO.
kubectl apply -f KafkaConnectConfig.yaml -n kafka

ECHO Apply Twitter Connection
ECHO.
kubectl apply -f TwitterCon.yaml -n kafka

ECHO.
ECHO Setup successful
pause