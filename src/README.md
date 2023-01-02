# Docker-compose project
### Development
```sh
# By default uses docker-compose.yml and overrides by docker-compose.override.yml
docker-compose up
```

### Production
```sh
docker-compose -f .\docker-compose.yml -f .\docker-compose.prod.yml up
```