ECHO off

ECHO "Creating gold-json sink connector"
ECHO .
"C:\Program Files\Git\bin\bash.exe" kafka_topic_sink_to_hdfs.sh "gold-json" "5" "10"
ECHO .

ECHO "Creating stock-json sink connector"
ECHO .
"C:\Program Files\Git\bin\bash.exe" kafka_topic_sink_to_hdfs.sh "stock-json" "5" "1000"
ECHO .

ECHO "Creating tweet-json sink connector"
ECHO .
"C:\Program Files\Git\bin\bash.exe" kafka_topic_sink_to_hdfs.sh "tweet-json" "5" "100"
ECHO .

pause