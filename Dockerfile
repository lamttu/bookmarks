# BUILD

FROM docker-mcr.artifactory.xero-support.com/dotnet/sdk:3.1 AS build

WORKDIR /app

COPY nuget.config ./
COPY ./Bookmark.API ./

RUN dotnet publish \
	--configuration Release \
	# --runtime linux-musl-x64 \
	# --self-contained true \
	--output out

# FINAL IMAGE

FROM docker-mcr.artifactory.xero-support.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
CMD [ "dotnet", "Bookmark.dll" ]