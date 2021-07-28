# Tributech DataSpace Kit Catalog API client

This repository contains the Catalog API client for easy access to the catalog-api of the Tributech DataSpace Kit.


## Integration via API

The client can be used to connect an application to the catalog api of the Tributech Dataspace Kit endpoints.


### Authentication / Authorization

You will need to to provide proper authorization in order to access the APIs. You can do this through client credentials authentication through the Keycloak server deployed at the hub of your ecoystem.
Check out [the example for how to do this in C#](./examples/netcore) through the ready to use [ApiAuthHandler](./clients/netcore/APIAuthHandler.cs) which we provide.
Also read the detailed guide for usage of the [.NET Core clients](./clients/netcore) for more info.

Essentially, you will need to provide the following parameters:
| Parameter | Value | Remark |
|-|-|-|
| tokenUrl | https://auth.your-hub.dataspace-hub.com/auth/realms/your-node/protocol/openid-connect/token | Url to retrieve the access token from the dataspace hub Keycloak Server |
| scope | profile / email / catalog-api / node-id | Defines the scope of what can be accessed |
| clientId | your-api-specific-client-id | Can be found in the Dataspace Admin (Profile -> Administration)
| clientSecret | your-api-specific-client-secret | Can be found in the Dataspace Admin (Profile -> Administration)

---

## Usage

Usage examples for the clients are available in [/examples](./examples).

You can inspect the availabe endpoints for the Catalog API Api through Swagger. Please follow the guide on [docs.tributech.io](https://docs.tributech.io/docs/integration/node/swagger-ui-authorization).

---

## Contribute

Please report any bugs / issues that you find, thank you!

---

## Need Support?

Use the [Tributech Support Center](https://tributech.atlassian.net/servicedesk/customer/portals).
