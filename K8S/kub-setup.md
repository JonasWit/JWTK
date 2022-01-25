### Docker Commands

docker build -t systemywp/master:[tag name] .
docker push systemywp/master:[tag name]

### Private Docker

docker login - locally
kubectl create secret generic [docker-key or some name] \
--from-file=.dockerconfigjson=.docker/config.json \
--type=kubernetes.io/dockerconfigjson

### Setup kubectl

export KUBECONFIG=test-kubeconfig.yaml
export KUBE_EDITOR=vim

### Add Secret for .NET

kubectl create secret generic secret-appsettings --from-file=./appsettings.secrets.json

### Create PVC

kubectl create -f pvc.yaml
kubectl get pvc

###  Deploy postgres

### Deploy microservices

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
kubectl create ing [ingress name] --rule=[our domain]/*=[service name]:[service port ex.80],tls=[secret with certificate name]
kubectl get ing