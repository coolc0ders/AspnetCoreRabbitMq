# Rabbitmq and ASP.net core demo using Masstransit

## Runing rabbit mq in a docker container:
docker run -d -t -it --hostname my-rabbitmq --name rabbitmq3-server -p 15672:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password -e RABBITMQ_DEFAULT_VHOST=my_vhost rabbitmq:3-management
