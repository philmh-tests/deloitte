# Deloitte - Developer - Integration Scenario

## Scenario
- The client has a simple scenario
- REST countries API (https://restcountries.eu/#api-endpoints-all ) returns detailed country information
- OpenWeatherMap REST API (https://openweathermap.org ) returns detailed weather information for a particular city
- The client needs a new REST API which will:
-- Support CRUD operations for a city 
-- Combine weather and country data for a particular city when searching
## Requirements
- Create a C# REST API that supports the following operations
-- Add City - adds city name, state (i.e. geographic sub-region), country, tourist rating (1-5), date established and estimated population. Adds record to local SQL data store and generates unique city id.
-- Update city – update rating, date established and estimated population by city id
-- Delete city – delete city by city id
-- Search city – search by city name, and returns the city id, name, state (i.e. geographic sub-region), country, tourist rating (1-5), date established, estimated population, 2 digit country code, 3 digit country code, currency code and weather for the city. If there are multiple matches, this information is returned for all matches. If the city is not stored locally no results need be returned. The APIs above should be used to provide any information not stored locally.
- The Add, Update and Delete operations will take place against a local SQL data store
- Provide at least 1 unit test
## Non-Functional Requirements
- Structure your code using modern development practices, the type you’d be proud to see in production

## Developer's Notes
- The URL for the REST Countries no longer exists; I used https://restcountries.com/v3.1/name/:nameTerm
- The Open Weather Map API expects the Latitude & Longitude which I had to determine for a City by using their Geocoding API
- I have provided an export from Postman with some of the tests executed
- Exception handling is the default, but this could be done globally using an `IExceptionFilter`
- Validation is light, but this could be handled in using Action Filters, or bespoke Validation Services at the service layer