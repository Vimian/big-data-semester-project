ECHO off
cls

ECHO Uninstall HDFS
ECHO.

cd hdfs

kubectl delete -f hdfs-cli.yaml -n stackable

kubectl delete -f zk.yaml -n stackable

kubectl delete -f znode.yaml -n stackable

kubectl delete -f hdfs.yaml -n stackable

helm uninstall zookeeper-operator

kubectl delete namespace stackable

cd ..

ECHO.
ECHO Uninstall successful
pause