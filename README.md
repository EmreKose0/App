-Mkdir Opt/app_docs/dotnetproject/containers
------------------docker-compose.yml--------------------
version: '3.4'
networks:
  dev:
    driver: bridge
services:
  app:
    image: docker.io/library/app
    depends_on:
      - "app_db"
    container_name: app-services
    ports:
      - "8088:80"
    build:
      context: .
      dockerfile: Dockerfile
    environment:
      - ConnectionStrings__DefaultConnection=User ID=postgres;Password=postgres;Server=app_db;Port=5432;Database=SampleDbDriver; IntegratedSecurity=true; Pooling=true;
      - ASPNETCORE_URLS=http://+:80
    networks:
      - dev
  app_db:
    image: postgres:lastest
    container_name: app_db
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
      - POSTGRES_DB=SampleDbDriver
    ports:
      - "5433:5432"
    restart: always
    volumes:
      - app_data:/var/lib/postgresql/data
    networks:
      - dev
volumes:
  app_data:
-------------------------------------------------------------------------

-docker-compose up -d 



