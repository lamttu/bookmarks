# UPSTREAM
# ------------------------------------------------------------------------------
# We alias all source images into upstream stages so there
# is a single place to bump image tags in this Dockerfile.
ARG ALPINE_VERSION=3.12
ARG DOCKER_REGISTRY="docker-hub.artifactory.xero-support.com/"
ARG DOCKER_REGISTRY_DOTNET="docker-mcr.artifactory.xero-support.com/"
ARG ASPNETCORE_SDK=3.1
ARG ASPNETCORE_RUNTIME=3.1.10

FROM ${DOCKER_REGISTRY_DOTNET}dotnet/core/sdk:${ASPNETCORE_SDK}-alpine AS upstream-dotnet-sdk
FROM ${DOCKER_REGISTRY_DOTNET}dotnet/core/runtime-deps:${ASPNETCORE_RUNTIME}-alpine${ALPINE_VERSION} AS upstream-dotnet-runtime

# BUILD
# ------------------------------------------------------------------------------
# Here we build the application into a self contained application that we will
# insert into a dotnet/core/runtime-deps image. Pstore and Newrelic binaries
# that are required for the final image are also downloaded in parallel.
FROM upstream-dotnet-sdk AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /app/utils

# ARG ENTERAWS_VERSION=v0.0.19
# RUN wget https://artifactory.xero-support.com/xero_artifacts/enteraws/${ENTERAWS_VERSION}/linux/enteraws -O ./enteraws && \
#     chmod +x ./enteraws

WORKDIR /app
COPY ./NuGet.Config ./
COPY ./Bookmark.API ./
RUN dotnet publish \
    --configuration ${BUILD_CONFIGURATION} \
    --runtime linux-musl-x64 \
    --self-contained true \
    --output out

# FINAL IMAGE
# ------------------------------------------------------------------------------
# This stage represents the final image that will be pushed to Artifactory.
FROM upstream-dotnet-runtime
ENV ASPNETCORE_URLS "http://0.0.0.0:80/"

RUN apk upgrade e2fsprogs
RUN apk upgrade openssl

COPY --from=build /app/out /app
# ENTRYPOINT ["/usr/bin/enteraws"]
CMD ["/app/Bookmark"]
WORKDIR /app