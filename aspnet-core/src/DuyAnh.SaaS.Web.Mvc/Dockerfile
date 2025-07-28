FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/DuyAnh.SaaS.Web.Mvc/DuyAnh.SaaS.Web.Mvc.csproj", "src/DuyAnh.SaaS.Web.Mvc/"]
COPY ["src/DuyAnh.SaaS.Web.Core/DuyAnh.SaaS.Web.Core.csproj", "src/DuyAnh.SaaS.Web.Core/"]
COPY ["src/DuyAnh.SaaS.Application/DuyAnh.SaaS.Application.csproj", "src/DuyAnh.SaaS.Application/"]
COPY ["src/DuyAnh.SaaS.Core/DuyAnh.SaaS.Core.csproj", "src/DuyAnh.SaaS.Core/"]
COPY ["src/DuyAnh.SaaS.EntityFrameworkCore/DuyAnh.SaaS.EntityFrameworkCore.csproj", "src/DuyAnh.SaaS.EntityFrameworkCore/"]
WORKDIR "/src/src/DuyAnh.SaaS.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/DuyAnh.SaaS.Web.Mvc", "src/DuyAnh.SaaS.Web.Mvc"]
COPY ["src/DuyAnh.SaaS.Web.Core", "src/DuyAnh.SaaS.Web.Core"]
COPY ["src/DuyAnh.SaaS.Application", "src/DuyAnh.SaaS.Application"]
COPY ["src/DuyAnh.SaaS.Core", "src/DuyAnh.SaaS.Core"]
COPY ["src/DuyAnh.SaaS.EntityFrameworkCore", "src/DuyAnh.SaaS.EntityFrameworkCore"]
WORKDIR "/src/src/DuyAnh.SaaS.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DuyAnh.SaaS.Web.Mvc.dll"]
