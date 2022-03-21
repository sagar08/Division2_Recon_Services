# DIVISION2 RECON - Rest API Service
The project to is a demostation of working model of Rest API, that is responsible to fetch the database for database and also interact with UI maningling
managing request-response.

## Project Overview   

* Division2ReconService - Rest API service
* Division2ReconService.Test - Unit Testing Project
* Diagrams - Mermaid diagrams for documentation 
## Stack
### IDE
* Visual Studio 2022
* SQLite Studio
### Framework & Packages
* Architecture - Monolithic 
* Clear Architecuture - To organize application of moderate to high complexity.
* Database - SQLite
* ORM - EF 6.0 (Database first approach)
* NLog  - for logging
* XUnit - Unit Testing application 
* Swagger - API Documentation
* Health Checks -  Middleware for reporting the health of application
* AutoMapper - object-object mapper
* Seed data - Initial data creation thorough external JSON file.
* Global Exception - workflow designed to determine the project's behavior when encountering an execution error.

## Configuration
* Division2ReconService.Test
>> CustomerControllerTest.cs - Configuration needed to set database file path.