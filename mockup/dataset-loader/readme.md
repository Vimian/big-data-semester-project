# create dataset loader

```
docker build -t dataset-loader .
```

```
kubectl create namespace mockup
kubectl apply -f dataset-loader.yaml -n mockup
```

# clean up

```
kubectl delete -f dataset-loader.yaml -n mockup
kubectl delete namespace mockup
```
