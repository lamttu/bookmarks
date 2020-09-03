# https://stackoverflow.com/questions/34751814/build-postgres-docker-container-with-initial-schema

FROM postgres:12.4

ENV POSTGRES_DB bookmarks-db

COPY ./setup.sql /docker-entrypoint-initdb.d/