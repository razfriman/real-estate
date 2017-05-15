FROM microsoft/aspnetcore-build
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
WORKDIR app.api

#RUN ["dotnet", "ef", "database", "update"]

EXPOSE 80/tcp

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

#ENTRYPOINT ["dotnet", "run"]

