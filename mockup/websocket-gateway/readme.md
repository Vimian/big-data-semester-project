# # create dataset loader

```
docker build -t websocket-gateway -f ./Dockerfile .
```

```
kubectl create namespace mockup
kubectl apply -f ./websocket-gateway.yaml -n mockup
```

# port forward

```
kubectl port-forward pods/websocket-gateway 3001:8080 -n mockup
```

# clean up

```
kubectl delete -f ./websocket-gateway.yaml -n mockup
kubectl delete namespace mockup
```
