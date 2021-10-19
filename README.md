# Using Service Connector to connect Azure WebApp with Azure Storage Blob

The repository offers the sample codes of connecting Azure Storage Blob to Azure WebApp with `system managed identity`. Follow the [steps](#getting-started) below to create and verify the connection.

## Getting Started

### 1. Prerequisites

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?ref=microsoft.com&utm_source=microsoft.com&utm_medium=docs&utm_campaign=visualstudio).
- Install the <a href="/cli/azure/install-azure-cli" target="_blank">Azure CLI</a> 2.18.0 or higher to provision and configure Azure resources.
- Sign in to Azure with CLI command

```azurecli
az login
```

### 2. Clone the sample codes

Clone the sample repository:
```terminal
git clone https://github.com/Azure-Samples/serviceconnector-webapp-storageblob-dotnet.git
```

### 3. Create App Service 
Go to the root folder of repository:
```terminal
cd WebAppStorageMISample
```

Create the webapp use webapp up:
```terminal
az webapp up --name WebAPPStorageMISample --sku B1 --location eastus
```

### 4. Create Azure Storage
```terminal
az storage account create --name storageforwebappsample --resource-group {rg_name} --sku Standard_RAGRS --https-only
```

### 5. Create the connection
```terminal
az webapp connection create storage-blob -g {rg_name} -n WebAPPStorageMISample --tg {rg_name} --account storageforwebappsample --system-identity
```

### 6. Validate the connection
Open the url https://webappstoragemisample.azurewebsites.net/.
You will see `Hello Resource Connector! Current is {UTC Time Now}.`
