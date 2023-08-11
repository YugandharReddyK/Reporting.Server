FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build-env

WORKDIR /app
COPY . /app
RUN nuget.exe restore .\Sperry.MxS.Reporting.Server.sln -Verbosity normal
RUN MSBuild.exe .\Sperry.MxS.Reporting.Server\Sperry.MxS.Reporting.Server.csproj /t:build /p:Configuration=Release /p:OutputPath=/app/out

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["Sperry.MxS.Reporting.Server.exe"]