ECHO off
cls

ECHO Setup dataset loader
ECHO.

docker build -t dataset-loader -f ./mockup/dataset-loader/Dockerfile .

kubectl create namespace mockup
kubectl apply -f ./mockup/dataset-loader/dataset-loader.yaml -n mockup

ECHO.
ECHO Setup dataset loader successful
pause