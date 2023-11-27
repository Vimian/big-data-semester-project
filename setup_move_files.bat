ECHO off
cls

ECHO Move files to HDFS
cd spark

kubectl -n stackable exec hdfs-cli -- mkdir spark-jobs
kubectl -n stackable exec hdfs-cli -- chmod 777 spark-jobs
kubectl -n stackable cp ./pi.py hdfs-cli:/opt/hadoop/spark-jobs/pi.py
kubectl -n stackable exec hdfs-cli -- hdfs dfs -put ./jobs /spark-jobs

pause