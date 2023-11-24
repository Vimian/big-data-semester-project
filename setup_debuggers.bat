ECHO off
cls

ECHO Setup HDFS debugger
ECHO.

cd hdfs

kubectl apply -f hdfs-cli.yaml -n stackable

cd ..

ECHO Setup Spark debugger
ECHO.

cd spark

kubectl apply -f spark-history-server.yaml -n stackable

cd ..

ECHO.
ECHO Setup debuggers successful
pause