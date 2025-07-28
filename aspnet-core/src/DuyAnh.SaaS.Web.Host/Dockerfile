FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/DuyAnh.SaaS.Web.Host/DuyAnh.SaaS.Web.Host.csproj", "src/DuyAnh.SaaS.Web.Host/"]
COPY ["src/DuyAnh.SaaS.Web.Core/DuyAnh.SaaS.Web.Core.csproj", "src/DuyAnh.SaaS.Web.Core/"]
COPY ["src/DuyAnh.SaaS.Application/DuyAnh.SaaS.Application.csproj", "src/DuyAnh.SaaS.Application/"]
COPY ["src/DuyAnh.SaaS.Core/DuyAnh.SaaS.Core.csproj", "src/DuyAnh.SaaS.Core/"]
COPY ["src/DuyAnh.SaaS.EntityFrameworkCore/DuyAnh.SaaS.EntityFrameworkCore.csproj", "src/DuyAnh.SaaS.EntityFrameworkCore/"]
WORKDIR "/src/src/DuyAnh.SaaS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/DuyAnh.SaaS.Web.Host", "src/DuyAnh.SaaS.Web.Host"]
COPY ["src/DuyAnh.SaaS.Web.Core", "src/DuyAnh.SaaS.Web.Core"]
COPY ["src/DuyAnh.SaaS.Application", "src/DuyAnh.SaaS.Application"]
COPY ["src/DuyAnh.SaaS.Core", "src/DuyAnh.SaaS.Core"]
COPY ["src/DuyAnh.SaaS.EntityFrameworkCore", "src/DuyAnh.SaaS.EntityFrameworkCore"]
WORKDIR "/src/src/DuyAnh.SaaS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DuyAnh.SaaS.Web.Host.dll"]
