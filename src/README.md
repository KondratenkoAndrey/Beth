# Docker-compose project
### Development
```sh
# By default uses docker-compose.yml and overrides by docker-compose.override.yml
docker-compose -p beth up
docker-compose -p beth down
```

### Production
```sh
docker-compose -f .\docker-compose.yml -f .\docker-compose.prod.yml up
```