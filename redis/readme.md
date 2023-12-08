# create redis operator

```
helm repo add ot-helm https://ot-container-kit.github.io/helm-charts/
helm repo update
```

```
kubectl create namespace redis
```

```
helm install redis-operator ot-helm/redis-operator -n redis
```

# create redis cluster

```
kubectl apply -f redis.yaml -n redis
```

# dev install

```
kubectl apply -f redisinsight.yaml -n redis
```

```
kubectl port-forward svc/redisinsight-service  8001:8001 -n redis
```

# clean up

```
kubectl delete -f redis.yaml -n redis
kubectl delete -f redisinsight.yaml -n redis
helm uninstall redis-operator -n redis
kubectl delete namespace redis
```
