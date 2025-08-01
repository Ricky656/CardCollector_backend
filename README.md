# Card Collector Backend

The Card Collector is an example project of a generic card collection application, made up of a backend created with C# Asp.Net and a frontend created with React JavaScript.

I am actively working on this app, there are currently missing features and sections that require refactoring. 

## Introduction

This is the backend API of the project, it handles the database and business logic. It can be run without using the frontend, in which case you can use the Swagger UI to interact with the api. By default this address is: 
* https://localhost:7155/swagger

However many endpoints require authorization via JWTs, this is currently not provided in the Swagger UI, so to explore the full functionality I recommend installing and using the CardCollector_frontend.

[Frontend Repository](https://github.com/Ricky656/CardCollector_frontend)

## Tech Stack

* C#
* .Net (9.0)
* Entity Framework Core (9.0.6)
* SQLite
* Swashbuckle (9.0.3)

## Local Installation

Install .NET 9.0 SDK

Clone this repository to your desired location

*(Optional)* Install dotnet-ef to use Entity Framework CLI commands: `dotnet tool install --global dotnet-ef`

Install local development certificate: `dotnet dev-certs https --trust`

Run the program with `dotnet run`, this will automatically install dependencies, create the database, and run the seeds found in Data/Seeder.cs. The database can also be found in the Data folder once created. 

By default this program will run at https://localhost:7155, this can be modified in the Properties/launchSettings.json file.

The Seeder file will add some basic dummy data to demonstrate functionality, as well as two Users - one with the Admin role. You can find/modify the example login details in the Data/Seeder.cs file and use them to login to the CardCollector_frontend when both apps are running. 

Deleting the database, or running `dotnet ef database drop` (with the above optional EF CLI commands installed), and then running the program will reset the database if desired. 

{address}/swagger for the swagger ui
