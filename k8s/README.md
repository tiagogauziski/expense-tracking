# Kubernetes manifest

## Cluster setup

1. Let's create the namespace we are going to use:
```bash
kubectl create namespace expense-tracking
```

1. We need a valid MySQL connection string to connect the API to the database:
```bash
kubectl -n expense-tracking create secret generic api-secrets --from-literal=connectionString="Server=localhost; User ID=root; Password=<PASSWORD>; Database=expenses" 
```

1. For the UI application, we need to create a `ConfigMap` to point to the correct API endpoint:
```bash
cat <<EOF | kubectl -n expense-tracking apply -f -
apiVersion: v1
kind: ConfigMap
metadata:
  name: ui-config
data:
  config.json: |
    {
      "baseUrl": "http://rasp4.home:8080"
    }
EOF
```

1. We can now deploy `k8s` manifests into the cluster:
```bash
kubectl -n expense-tracking apply -k https://github.com/tiagogauziski/expense-tracking.git/k8s
``` 
