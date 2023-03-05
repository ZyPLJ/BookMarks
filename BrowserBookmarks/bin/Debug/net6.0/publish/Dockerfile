#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 9031

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
#WORKDIR /src
#COPY ["BrowserBookmarks/BrowserBookmarks.csproj", "BrowserBookmarks/"]
#COPY ["BrowerBookmariks.Model/BrowerBookmariks.Model.csproj", "BrowerBookmariks.Model/"]
#COPY ["BrowerBookmariks.Services/BrowerBookmariks.Services.csproj", "BrowerBookmariks.Services/"]
#RUN dotnet restore "BrowserBookmarks/BrowserBookmarks.csproj"
#COPY . .
#WORKDIR "/src/BrowserBookmarks"
#RUN dotnet build "BrowserBookmarks.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "BrowserBookmarks.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrowserBookmarks.dll"]