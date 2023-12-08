ECHO Setup Kafka
ECHO.

cd ..

ECHO create operators
ECHO.

kubectl create namespace kafka

kubectl create -f "https://strimzi.io/install/latest?namespace=kafka" -n kafka

kubectl apply -f kafka.yaml -n kafka

ECHO create kafka cluster

kubectl apply -f kafka-connect.yaml -n kafka

cd connect

ECHO.
ECHO Building DockerDebug

docker build -t debug_mode -f DockerDebug .

ECHO.
ECHO Running DockerDebug

kubectl apply -f kafkaDebug-connect.yaml -n kafka

ECHO.
ECHO Setup successful
pause