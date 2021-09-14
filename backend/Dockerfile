FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.sln .
COPY Directory.Build.props .

# Copy all the project files
# Base projects
COPY BLL.Base/*.csproj ./BLL.Base/
COPY Emulator.BLL.Base/*.csproj ./Emulator.BLL.Base/
COPY Emulator.DAL.Base/*.csproj ./Emulator.DAL.Base/
COPY Emulator.Domain.Base/*.csproj ./Emulator.Domain.Base/
COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain.Base/*.csproj ./Domain.Base/
COPY Extensions.Base/*.csproj ./Extensions.Base/
COPY Resources.Base/*.csproj ./Resources.Base/

COPY TestProject/*.csproj ./TestProject/

# App projects
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain.App/*.csproj ./Domain.App/
COPY Emulator.BLL.App/*.csproj ./Emulator.BLL.App/
COPY Emulator.DAL.App/*.csproj ./Emulator.DAL.App/
COPY PublicApi.DTO.v1/*.csproj ./PublicApi.DTO.v1/
COPY Resources/*.csproj ./Resources/
COPY WebApp/*.csproj ./WebApp/

# Restore all NuGet packages
RUN dotnet restore

# Copy over the source code
# Base projects
COPY BLL.Base/. ./BLL.Base/
COPY Emulator.BLL.Base/. ./Emulator.BLL.Base/
COPY Emulator.DAL.Base/. ./Emulator.DAL.Base/
COPY Emulator.Domain.Base/. ./Emulator.Domain.Base/
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain.Base/. ./Domain.Base/
COPY Extensions.Base/. ./Extensions.Base/

COPY TestProject/. ./TestProject/

# App projects
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain.App/. ./Domain.App/
COPY Emulator.BLL.App/. ./Emulator.BLL.App/
COPY Emulator.DAL.App/. ./Emulator.DAL.App/
COPY PublicApi.DTO.v1/. ./PublicApi.DTO.v1/
COPY Resources/. ./Resources/
COPY WebApp/. ./WebApp/

WORKDIR /source/WebApp

RUN dotnet publish -c Release -o out

# Create new image from aspnet runtime (no compilers)
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
COPY --from=build /source/WebApp/out/ ./

ENTRYPOINT ["dotnet", "WebApp.dll"]