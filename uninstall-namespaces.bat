ECHO off

ECHO Delete namespaces
ECHO.

kubectl create namespace stackable
kubectl create namespace kafka
kubectl create namespace redis
kubectl create namespace mockup

kubectl delete namespace stackable
kubectl delete namespace kafka
kubectl delete namespace redis
kubectl delete namespace mockup

ECHO.
ECHO Successfully deleted namespaces
pause