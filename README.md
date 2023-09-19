# Countries API

Countries API is responsible for getting all countries from API
and perform filtering, sorting and pagination on the 
provided data.

## Getting Started
1. Prerequisites and Installation
        Visual Studio 2022 or VS Code installed or any other IDE or code editor.
        You can download and install the latest version of Visual Studio from the official website.
2. dotnet sdk 6 
    If you are not using Visual Studio 2022 you can download and install the sdk from the official website 

3. Clone repository

## Run locally
1. Go to repository root
2. Change directory to ./CountriesAPI
3. Execute dotnet run

## How to test endpoint locally?

API supports only following endpoint <b>/countries</b>
<image src="./endpoint.png">

Examples with curl:

1. Without parameters - if no parameters are provided API will return all countries, eg <b>curl 'https://localhost:5000/countries' -UseBasicParsing</b>

2. Data can be sorted in ascending or descending order
Ascending order - <b>curl 'https://localhost:7018/countries?sort=ascend' -UseBasicParsing</b>

Descending order - <b>curl 'https://localhost:7018/countries?sort=descend' -UseBasicParsing</b>

3. Pagination - in order to perform pagination <b>limit</b>
and <b>offset</b> fields should be used. Limit field specifies how many items should be returned. Example: If I want to take first 15 
items from API then it can use the following <b>curl 'https://localhost:7018/countries?limit=15' -UseBasicParsing</b>
Offset field should be used to specify how many items should be skipped before starting to take items, example: If I want 15 items, but skip the first 10, then it should look like this <b>curl 'https://localhost:7018/countries?limit=10&offset=10' -UseBasicParsing</b>

4. Filtering
API supports 3 options for filtering
1. Filter by name only. Filter for countries containing the substring specified by the user in the names. Search is case insentive.
Example: <b>curl 'https://localhost:7018/countries?filter=contains%28name%2C%20st%29' -UseBasicParsing</b> or the filter parameter should look like this <b>contains(name, st)</b>

2. Filter by population. Filter for countries with population less than provided.
Example: <b>curl 'https://localhost:7018/countries?filter=lessThan%28population%2C%2010%29' -UseBasicParsing</b>
or the filter parameter should look like this <b>lessThan(population, 10)</b>

3. Filter by name and population. 
Example: <b>curl 'https://localhost:7018/countries?filter=and%28lessThan%28population%2C%2010%29%2C%20contains%28name%2C%20st%29%29' -UseBasicParsing</b> 
or the filter parameter should look like this <b>and(lessThan(population, 10), contains(name, st))</b>

4. Using all parameters

Examples:

1. <b>curl 'https://localhost:7018/countries?filter=and%28lessThan%28population%2C%2010%29%2C%20contains%28name%2C%20st%29%29&sort=ascend&limit=10&offset=15' -UseBasicParsing</b> 

2. <b>curl 'https://localhost:7018/countries?filter=and%28lessThan%28population%2C%2010%29%2C%20contains%28name%2C%20st%29%29&sort=descend&limit=10&offset=15' -UseBasicParsing</b> 
