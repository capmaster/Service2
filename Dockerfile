FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Api/*.csproj ./Api/
COPY External/*.csproj ./External/
COPY Model/*.csproj ./Model/
RUN dotnet restore "Api/Api.csproj"

# Copy everything else and build
COPY . ./
WORKDIR /app/Api

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/Api/out .
ENTRYPOINT ["dotnet", "Api.dll"]