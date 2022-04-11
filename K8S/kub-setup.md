### Docker Commands
docker system prune -a
dotnet publish -c Release -o publish

docker build -f SystemyWP.API/Dockerfile -t systemywp/master:gate_v2 .

docker build -t systemywp/master:gate_v2 .
docker build -t systemywp/master:gastronomy .
docker build -t systemywp/master:client_v1 .

docker push systemywp/master:gate_v2
docker push systemywp/master:gastronomy
docker push systemywp/master:client_v1

docker run -it -p 8080:8080 --rm --name dockerize-vuejs-app-1 systemywp/master:client_v1
docker run -d -p 8080:80 --name myapp systemywp/master:gate_v2

### Setup kubectl

export KUBECONFIG=core-kubeconfig.yaml
export KUBE_EDITOR=vim

# login to postgres
psql --username=user-name db-name

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

helm repo update

LINODE_API_TOKEN=1234abcd...6789

helm upgrade --install external-dns bitnami/external-dns \
--namespace external-dns --create-namespace \
--set provider=linode \
--set linode.apiToken=$LINODE_API_TOKEN

kubectl logs -n external-dns -l app.kubernetes.io/name=external-dns

// do this later on the cert step
kubectl annotate service [our service name] \
external-dns.alpha.kubernetes.io/hostname=api.systemywp.pl

### Install Ingress

helm upgrade --install traefik traefik/traefik \
--create-namespace --namespace traefik \
--set "ports.websecure.tls.enabled=true" \
--set "providers.kubernetesIngress.publishedService.enabled=true"

// do this later on the cert step
kubectl create ingress master-gate --rule=api.systemywp.pl/*=systemywp-master-srv:80
kubectl get ing

kubectl create ingress test-app --rule=api.systemywp.pl/*=test-app:80

### Enable TLS

helm upgrade --install cert-manager cert-manager \
--repo https://charts.jetstack.io \
--create-namespace --namespace cert-manager \
--set installCRDs=true

kubectl apply -f cm-clusterissuer.yaml
kns default - be sure that you are in default namespace
kubectl apply -f cm-certificate.yaml

// now create domain, ingress and certificate
kubectl create ing master-gate --rule=api.systemywp.pl/*=systemywp-master-srv:80,tls=api.systemywp.pl
kubectl annotate ingress master-gate cert-manager.io/cluster-issuer=letsencrypt-production

kubectl create ing client-app --rule=www.systemywp.pl/*=client-app-srv:80,tls=www.systemywp.pl
kubectl annotate ingress client-app cert-manager.io/cluster-issuer=letsencrypt-production

kubectl get ing