# GPlay API

### GPlay Service tree
```md
gplay.api.gateway : 5071
├─── auth.services : 5072
├─── gameplay.services : 5073
└─── notification.services : 5074

```

* 5070 : gplay-rabbitmq
* 5071 : gplay-api-gateway
* 5072 : gplay-auth-services
* 5074 : gplay-notification
* 5073 : gplay-gameplay-services
* 5080 : Seq -- log monitor
* 6379 : gplay-redis
* gplay-cms-services


### Docker Compose
```md

GPlay
├─── gplay.api.gateway
│   ├─── Dockerfile
│   ├─── docker-compose.yml
|
├─── gplay.auth.services
│   ├─── Dockerfile
│   ├─── docker-compose.yml
|
├─── gplay.gameplay.services
│   ├─── Dockerfile
│   ├─── docker-compose.yml
|
└─── gplay.notification.services
    ├─── Dockerfile
    ├─── docker-compose.yml

```


### API Routes
https://devgplayapi.gakktechnology.com/api

```md
api/    <-- gateway
|
├─── /auth/welcome
|
├─── /play/welcome
|
└─── /notification/welcome

```

### Welcome Routes
https://devgplayapi.gakktechnology.com/api/welcome

https://devgplayapi.gakktechnology.com/api/auth/welcome

https://devgplayapi.gakktechnology.com/api/play/welcome

https://devgplayapi.gakktechnology.com/api/notification/welcome


### Docker Compose Directory Structure
```md
project/     
|
├─── Dockerfile
|
├─── docker-compose.yml        # Production-specific settings
|
├─── docker-compose.dev.yml    # Development-specific settings
|
└─── .env                      # Environment variables

```

### OpenTelemetry Endpoints
https://devgplayapi.gakktechnology.com/metrics

https://devgplayapi.gakktechnology.com/api/auth/metrics



### APP Metrics
dotnet tool update -g dotnet-counters

dotnet-counters monitor -n GPlay.Auth.Services --counters Microsoft.AspNetCore.Hosting

### Postgres Console

ssh mayall@192.168.101.14
asdfasdf
sudo docker exec -it gplay-postgres psql -U gplayDev -d userdb

### Container network
docker network create gplay-net

### Postgres
docker run --name postgres -p 5432:5432 --network gplay-net -e POSTGRES_PASSWORD=g@kk1215 -d postgres

### RabbitMQ
docker run -it --name rabbitmq --network gplay-net -p 5672:5672 -p 15672:15672 rabbitmq:4.0-management


### Redis
docker run -d --name redis --network gplay-net -p 6379:6379 redis/redis-stack-server:latest


### GPlay Auth Service
docker run -it --name gplay-auth-services --network gplay-net -p 3300:3300 gplay-auth-services


### Backend Sub-domain
devgplayapi.gakktechnology.com



### Azure Cloud

az acr login -n mayall

