FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MsmePortal.csproj", "./"]
RUN dotnet restore "MsmePortal.csproj"
COPY . .
RUN dotnet publish "MsmePortal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src /src-temp

# Mirror all Razor views into all casing variations to guarantee 100% Linux view resolution
RUN mkdir -p /app/Views/Account /app/Views/account /app/Views/Shared /app/Views/shared \
             /app/views/Account /app/views/account /app/views/Shared /app/views/shared && \
    find /src-temp -name "*[Ll]ogin*.cshtml" -exec cp {} /app/Views/Account/Login.cshtml \; -exec cp {} /app/Views/Account/login.cshtml \; -exec cp {} /app/Views/account/Login.cshtml \; -exec cp {} /app/Views/account/login.cshtml \; -exec cp {} /app/views/Account/Login.cshtml \; -exec cp {} /app/views/account/login.cshtml \; -exec cp {} /app/Views/Shared/Login.cshtml \; -exec cp {} /app/views/shared/login.cshtml \; 2>/dev/null || true && \
    cp -rn /src-temp/Views/* /app/Views/ 2>/dev/null || true && \
    cp -rn /src-temp/views/* /app/views/ 2>/dev/null || true && \
    rm -rf /src-temp

ENV ASPNETCORE_HTTP_PORTS=8080
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
EXPOSE 8080

ENTRYPOINT ["dotnet", "MsmePortal.dll"]
