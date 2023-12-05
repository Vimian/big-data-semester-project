# create dataset loader

```
docker build -t dataset-loader -f ./mockup/dataset-loader/Dockerfile .
```

```
kubectl create namespace mockup
kubectl apply -f ./mockup/dataset-loader/dataset-loader.yaml -n mockup
```

# clean up

```
kubectl delete -f ./mockup/dataset-loader/dataset-loader.yaml -n mockup
kubectl delete namespace mockup
```
