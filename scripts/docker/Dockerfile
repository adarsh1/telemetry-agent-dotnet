FROM microsoft/dotnet:2.0.3-runtime-jessie

MAINTAINER Devis Lucato (https://github.com/dluc)

LABEL Tags="Azure,IoT,Solutions,IoT Hub,Telemetry,Analytics,.NET"

ENTRYPOINT ["/bin/bash", "/app/run.sh"]

ARG user=pcsuser

RUN useradd -m -s /bin/bash -U $user

COPY . /app/
RUN chown -R $user.$user /app
WORKDIR /app

USER $user
