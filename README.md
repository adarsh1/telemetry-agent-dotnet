[![Build][build-badge]][build-url]
[![Issues][issues-badge]][issues-url]
[![Gitter][gitter-badge]][gitter-url]

Telemetry Agent Overview
========================

This service analyzes the telemetry stream, stores messages from Azure IoT Hub
to DocumentDb, and generates alerts according to defined rules.
The IoT stream is analyzed using a set of rules defined in the
[Telemetry service](https://github.com/Azure/device-telemetry-dotnet),
and generates "alarms" when a message matches some of these rules. The alarms
are also stored in DocumentDb.

We provide also a
[Java version](https://github.com/Azure/telemetry-agent-java)
of this project.

Dependencies
============
* [Azure Cosmos DB account](https://ms.portal.azure.com/#create/Microsoft.DocumentDB) use the one created for [Storage Adapter microservice](https://github.com/Azure/pcs-storage-adapter-dotnet)
* [Telemetry](https://github.com/Azure/device-telemetry-dotnet)
* [Config](https://github.com/Azure/pcs-config-dotnet)
* [IoT Hub Manager](https://github.com/Azure/iothub-manager-dotnet)
* [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub) use the one created for [IoT Hub Manager](https://github.com/Azure/iothub-manager-dotnet)

How to use the microservice
===========================

## Quickstart - Running the service with Docker

1. Install Docker Compose: https://docs.docker.com/compose/install
1. Create an instance of an [Azure IoT Hub](https://azure.microsoft.com/services/iot-hub)
1. Store the "IoT Hub" connection string  in the [env-vars-setup](scripts)
   script.
1. Open a terminal console into the project folder, and run these commands to start
   the [Telemetry Agent](https://github.com/Azure/telemetry-agent-dotnet) service
   ```
   cd scripts
   env-vars-setup      // (Bash users: ./env-vars-setup).  This script creates your env. variables
   cd docker
   docker-compose up
   ```
The Docker compose configuration requires the [dependencies](README.md#dependencies) resolved and
environment variables set as described previously. You should now start seeing the stream
content in the console.

## Running the service with Visual Studio or VS Code

1. [Install .NET Core 2.x][dotnet-install]
1. Install any recent edition of Visual Studio (Windows/MacOS) or Visual
   Studio Code (Windows/MacOS/Linux).
   * If you already have Visual Studio installed, then ensure you have
   [.NET Core Tools for Visual Studio 2017][dotnetcore-tools-url]
   installed (Windows only).
   * If you already have VS Code installed, then ensure you have the [C# for Visual Studio Code (powered by OmniSharp)][omnisharp-url] extension installed.
1. Open the solution in Visual Studio or VS Code.
1. Define the following environment variables. See [Configuration and Environment variables](#configuration-and-environment-variables) for detailed information for setting these for your enviroment.
   1. `PCS_IOTHUB_CONNSTRING` = {your Azure IoT Hub connection string}
   1. `PCS_IOTHUB_PARTITIONS` = {your Azure IoT Hub partitions count}
   1. `PCS_STORAGEADAPTER_WEBSERVICE_URL` = http://localhost:9022/v1
   1. etc...
1. Start the WebService project (e.g. press F5).
1. Use an HTTP client such as [Postman][postman-url], to exercise the
   RESTful API.

# Configuration and Environment variables

The service configuration is accessed via ASP.NET Core configuration
adapters, and stored in [appsettings.ini](WebService/appsettings.ini).
The INI format allows to store values in a readable format, with comments.

The configuration also supports references to environment variables, e.g. to
import credentials and network details. Environment variables are not
mandatory though, you can for example edit appsettings.ini and write
credentials directly in the file. Just be careful not sharing the changes,
e.g. sending a Pull Request or checking in the changes in git.

The configuration file in the repository references some environment
variables that need to be defined. Depending on the OS and the IDE used,
there are several ways to manage environment variables.

1. If you're using Visual Studio (Windows/MacOS), the environment
   variables are loaded from the project settings. Right click on WebService,
   and select Options/Properties, and find the section with the list of env
   vars. See [WebService/Properties/launchSettings.json](WebService/Properties/launchSettings.json).
1. Visual Studio Code (Windows/MacOS/Linux) loads the environment variables from
   [.vscode/launch.json](.vscode/launch.json)
1. When running the service **with Docker** or **from the command line**, the
   application will inherit environment variables values from the system. 
   * [This page][windows-envvars-howto-url] describes how to setup env vars
     in Windows. We suggest to edit and execute once the
     [env-vars-setup.cmd](scripts/env-vars-setup.cmd) script included in the
     repository. The settings will persist across terminal sessions and reboots.
   * For Linux and MacOS, we suggest to edit and execute
     [env-vars-setup](scripts/env-vars-setup) each time, before starting the
     service. Depending on OS and terminal, there are ways to persist values
     globally, for more information these pages should help:
     * https://stackoverflow.com/questions/13046624/how-to-permanently-export-a-variable-in-linux
     * https://stackoverflow.com/questions/135688/setting-environment-variables-in-os-x
     * https://help.ubuntu.com/community/EnvironmentVariables

Contributing to the solution
============================
Please follow our [contribution guildelines](CONTRIBUTING.md).  We love PRs too.

Troubleshooting
===============

Feedback
========
Please enter issues, bugs, or suggestions as GitHub Issues [here](https://github.com/Azure/telemetry-agent-dotnet/issues)

[build-badge]: https://img.shields.io/travis/Azure/telemetry-agent-dotnet.svg
[build-url]: https://travis-ci.org/Azure/telemetry-agent-dotnet
[issues-badge]: https://img.shields.io/github/issues/azure/telemetry-agent-dotnet.svg
[issues-url]: https://github.com/azure/telemetry-agent-dotnet/issues
[gitter-badge]: https://img.shields.io/gitter/room/azure/iot-solutions.js.svg
[gitter-url]: https://gitter.im/azure/iot-solutions



[run-with-docker-url]:(https://docs.microsoft.com/azure/iot-suite/iot-suite-remote-monitoring-deploy-local#run-the-microservices-in-docker)
[rm-arch-url]:https://docs.microsoft.com/en-us/azure/iot-suite/iot-suite-remote-monitoring-sample-walkthrough
[postman-url]: https://www.getpostman.com
[dotnet-install]: https://www.microsoft.com/net/learn/get-started
[vs-install-url]: https://www.visualstudio.com/downloads
[dotnetcore-tools-url]: https://www.microsoft.com/net/core#windowsvs2017
[omnisharp-url]: https://github.com/OmniSharp/omnisharp-vscode
[windows-envvars-howto-url]: https://superuser.com/questions/949560/how-do-i-set-system-environment-variables-in-windows-10
