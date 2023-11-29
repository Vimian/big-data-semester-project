ECHO off
cls

cd dataset

ECHO Upload stock data

bash upload_to_minio.sh "datasets" "Stock" "127.0.0.1:9001" "admin" "password" "/datasets/Stock"

pause