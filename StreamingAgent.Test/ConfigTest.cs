// Copyright (c) Microsoft. All rights reserved.

using Microsoft.Azure.IoTSolutions.IoTStreamAnalytics.Services.Exceptions;
using Microsoft.Azure.IoTSolutions.IoTStreamAnalytics.StreamingAgent.Runtime;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace StreamingAgent.Test
{
    public class ConfigTest
	{
		public ConfigTest()
		{
		}

		[Fact]
		public async Task ParseHubEndpointWithEndpointPrefix()
		{
            // Arrange
            const string hubEndpointInputString = "Endpoint=sb://iothub-ns-iothub-123-1232-123.servicebus.windows.net/;SharedAccessKeyName=iothubowner;";
            const string hubEndpointParsedString = "sb://iothub-ns-iothub-123-1232-123.servicebus.windows.net/";
            Mock<IConfigData> configData = new Mock<IConfigData>();
			configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
			configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
			configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("AzureBlob");
			configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpointInputString);

			// Act
			Config config = new Config(configData.Object);

			// Assert
			Assert.Equal(hubEndpointParsedString, config.IoTHubConfig.ConnectionConfig.HubEndpoint);
		}

		[Fact]
		public async Task ParseHubEndpointWithoutEndpointPrefix()
		{
			// Arrange
			const string hubEndpoint = "sb://iothub-ns-iothub-123-1232-123.servicebus.windows.net/";
			Mock<IConfigData> configData = new Mock<IConfigData>();
			configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
			configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
			configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("AzureBlob");
			configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpoint);

			// Act
			Config config = new Config(configData.Object);

			// Assert
			Assert.Equal(hubEndpoint, config.IoTHubConfig.ConnectionConfig.HubEndpoint);
		}

        [Fact]
        public async Task ThrowExceptionForInvalidHubEndpoint()
        {
            // Arrange
            const string hubEndpoint = "url=iothub-ns-iothub-123-1232-123";
            Mock<IConfigData> configData = new Mock<IConfigData>();
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("AzureBlob");
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpoint);

            // Act, Assert
            Assert.Throws<InvalidConfigurationException>(() => new Config(configData.Object));
        }

        [Fact]
        public async Task ThrowExceptionForMissingHubName()
        {
            // Arrange
            const string hubEndpoint = "url=iothub-ns-iothub-123-1232-123";
            Mock<IConfigData> configData = new Mock<IConfigData>();
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("AzureBlob");
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpoint);

            // Act, Assert
            Assert.Throws<InvalidConfigurationException>(() => new Config(configData.Object));
        }

        [Fact]
        public async Task ThrowExceptionForMissingStorageNamespace()
        {
            // Arrange
            const string hubEndpoint = "url=iothub-ns-iothub-123-1232-123";
            Mock<IConfigData> configData = new Mock<IConfigData>();
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("AzureBlob");
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpoint);

            // Act, Assert
            Assert.Throws<InvalidConfigurationException>(() => new Config(configData.Object));
        }

        [Fact]
        public async Task ThrowExceptionForInvalidStorageBackendType()
        {
            // Arrange
            const string hubEndpoint = "url=iothub-ns-iothub-123-1232-123";
            Mock<IConfigData> configData = new Mock<IConfigData>();
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("InvalidBlob");
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns(hubEndpoint);

            // Act, Assert
            Assert.Throws<InvalidConfigurationException>(() => new Config(configData.Object));
        }

        [Fact]
        public async Task ThrowExceptionForMissingHubEndpoint()
        {
            // Arrange
            Mock<IConfigData> configData = new Mock<IConfigData>();
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubName")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:namespace")).Returns("iothub123");
            configData.Setup(x => x.GetString("telemetryagent:iothub:checkpointing:storage:backendType")).Returns("InvalidBlob");
            configData.Setup(x => x.GetString("telemetryagent:iothub:connection:hubEndpoint")).Returns("");

            // Act, Assert
            Assert.Throws<InvalidConfigurationException>(() => new Config(configData.Object));
        }
    }
}
