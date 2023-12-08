kubectl delete -f kafkaDebug-connect.yaml -n kafka
kubectl delete -f KafkaConnectConfig.yaml -n kafka
kubectl delete -f TwitterCon.yaml -n kafka

ECHO.
ECHO Uninstall successful
pause