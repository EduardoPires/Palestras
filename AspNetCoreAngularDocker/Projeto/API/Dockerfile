### Estágio 1 - Obter o source e gerar o Build ###
FROM microsoft/aspnetcore-build:2.0 AS dotnet-builder
WORKDIR /app
COPY . /app
RUN dotnet restore src/MeetupAPI/MeetupAPI.csproj
RUN dotnet publish src/MeetupAPI/MeetupAPI.csproj -c Release -o /app/publish 

### Estágio 2 - Subir a aplicação através dos binários ###
FROM microsoft/aspnetcore:2.0
WORKDIR /app
ENV ASPNETCORE_URLS http://*:57332
EXPOSE 57332
COPY --from=dotnet-builder /app/publish .
ENTRYPOINT ["dotnet", "MeetupAPI.dll"]