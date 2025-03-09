0) Install docker
1) Create docker network:
`docker network create mynetwork`
2) Create and run mssql container:
`docker run -p 1433:1433 --name mssql -e SA_PASSWORD="QWEqwe123" -e ACCEPT_EULA=Y --network mynetwork -d mcr.microsoft.com/mssql/server:2022-latest`
3) Download the application
4) Set up a connection to the database in appsetting.json (If you have not changed the mssql creation settings, then appsetting.json no editing is required.)
5) Create application from dockerfile
`docker build -t UserIpSearcher .`
6) Run application contaner:
`docker run --name user_ip_searcher -p 8080:8080 --network mynetwork useripsearcher`
7) Getting:
http://localhost:8080/swagger/index.html