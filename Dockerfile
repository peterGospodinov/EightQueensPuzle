# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app


# Copy the project files and restore dependencies
COPY QueensPuzzle.Web/QueensPuzzle.Web.csproj ./QueensPuzzle.Web/
RUN dotnet restore ./QueensPuzzle.Web/QueensPuzzle.Web.csproj

# Copy the remaining files and publish the application
COPY . ./
RUN dotnet publish ./QueensPuzzle.Web -c Release -o out

# Use the official ASP.NET Core runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the application runs on
EXPOSE 80

# Set the entry point for the Docker container
ENTRYPOINT ["dotnet", "QueensPuzzle.dll"]