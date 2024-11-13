#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FitExerciseBack/FitExerciseBack.csproj", "FitExerciseBack/"]
RUN dotnet restore "FitExerciseBack/FitExerciseBack.csproj"
COPY . .
WORKDIR "/src/FitExerciseBack"
RUN dotnet build "FitExerciseBack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitExerciseBack.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitExerciseBack.dll"]