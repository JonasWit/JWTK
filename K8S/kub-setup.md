### Docker Commands

docker build -t systemywp/master:[tag name] .
docker push systemywp/master:[tag name]

### Setup kubectl

export KUBECONFIG=core-kubeconfig.yaml
export KUBE_EDITOR=vim

kubectl logs [pod-name] -p
kubectl describe pod [pod-name]

### Private Docker

docker login - locally
kubectl create secret generic docker-key \
--from-file=.dockerconfigjson=.docker/config.json \
--type=kubernetes.io/dockerconfigjson

### Add Secret for .NET

kubectl create secret generic secret-appsettings --from-file=./appsettings.secrets.json

### Deploy postgres

kubectl apply -f postgres-secrets.yaml
kubectl apply -f postgres-pv-claim.yaml
kubectl apply -f postgres.yaml

# enter the container
kubectl exec -it postgres-0 bash

# login to postgres
psql --username=swp_api init-db

### Deploy microservices & Tips

kubectl apply -f systemywp-master.yaml

base 64 password - echo -n 'my-super-secret-password' | base64
kubectl exec --stdin --tty [pod] -- /bin/bash
kubectl logs [pod-name]

##  Main API

###  External DNS

-> TTL as low as possible
-> Get token from Linode

helm repo add bitnami https://charts.bitnami.com/bitnami

LINODE_API_TOKEN=1234abcd...6789
helm upgrade --install external-dns bitnami/external-dns \
--namespace external-dns --create-namespace \
--set provider=linode \
--set linode.apiToken=$LINODE_API_TOKEN

// do this later on the cert step
kubectl annotate service [our service name] \
external-dns.alpha.kubernetes.io/hostname=api.systemywp.pl

### Install Ingress

helm upgrade --install traefik traefik/traefik \
--create-namespace --namespace traefik \
--set "ports.websecure.tls.enabled=true" \
--set "providers.kubernetesIngress.publishedService.enabled=true"

// do this later on the cert step
kubectl create ingress [ingress name] --rule=[our domain]/*=[service name]:[service port]
kubectl get ing

### Enable TLS

helm upgrade --install cert-manager cert-manager \
--repo https://charts.jetstack.io \
--create-namespace --namespace cert-manager \
--set installCRDs=true

kubectl apply -f cm-clusterissuer.yaml
kns default - be sure that you are in default namespace
kubectl apply -f cm-certificate.yaml

// now create domain, ingress and certificate
kubectl create ing [master_gate] --rule=[api.systemywp.pl]/*=[systemywp-master]:[80],tls=[api.systemywp.pl]
kubectl get ing