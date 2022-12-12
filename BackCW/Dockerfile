FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackCW/BackCW.csproj", "BackCW/"]
RUN dotnet restore "BackCW/BackCW.csproj"
COPY . .
WORKDIR "/src/BackCW"
RUN dotnet build "BackCW.csproj" -c Development -o /app/build

FROM build AS publish
RUN dotnet publish "BackCW.csproj" -c Development -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackCW.dll"]
