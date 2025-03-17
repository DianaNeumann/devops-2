### Description :

This a sample project Asp.Net Core API + MongoDb + Docker.

A CRUD Example is at your disposal.

### Project structure :
* Infrastructure folder contains the MongoDb Settings class that is bind to appsettings
* Models folder contains a View Model class for the example
* Services folder contains services class News CRUD example with MongoDriver

### Setup :


## Step 1 : Run the project

```bash
$ docker-compose up --build
```
* Open Mongo-Express, in your browser localhost:3000
* Create Database
* Create Collection
* Make sure that the names are matching with those in the appsettings.json
* Api endpoint is binded to port 5000 feel free to change it in docker-compose.yml or other port.
