ECHO off

kubectl port-forward svc/kafka-connect 8083:8083 -n kafka

pause