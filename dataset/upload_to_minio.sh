#!/bin/bash
bucket=$1
files=$2

host=$3
user=$4
password=$5

for file in /datasets/Stock/*
do
    ${file}
#    resource="/$6/${file}"
#    content_type="application/octet-stream"
#    date=`date -R`
#    _signature="PUT\n\n${content_type}\n${date}\n${resource}"
#    signature=`echo -en ${_signature} | openssl sha1 -hmac ${password} -binary | base64`

#    curl -X PUT -T "${file}" \
#        -H "Host: ${host}" \
#        -H "Date: ${date}" \
#        -H "Content-Type: ${content_type}" \
#        -H "Authorization: AWS ${user}:${signature}" \
#        http://${host}${resource}
done

