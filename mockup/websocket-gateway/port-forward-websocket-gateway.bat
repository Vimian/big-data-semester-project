ECHO off

kubectl port-forward pods/websocket-gateway 3001:8080 -n mockup

pause