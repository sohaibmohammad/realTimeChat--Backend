FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# «·„”«—«   »œ√ „»«‘—… „‰ «·„Ã·œ «·√» ··”Ì” „ ﬂ«„·«
COPY ["RealTimeChat/RealTimeChat.csproj", "RealTimeChat/"]
COPY ["Chat.Business/Chat.Business.csproj", "Chat.Business/"]
COPY ["Chat.Infrastructure/Chat.Infrastructure.csproj", "Chat.Infrastructure/"]
COPY ["ChatDomain/Chat.Domain.csproj", "ChatDomain/"]

RUN dotnet restore "RealTimeChat/RealTimeChat.csproj"
COPY . .
WORKDIR "/src/RealTimeChat"
RUN dotnet build "RealTimeChat.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RealTimeChat.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealTimeChat.dll"]