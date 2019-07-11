
| :mega: Important notice if you're upgrading between major versions! |
|--------------|
|* If you're upgrading from 4.x to 5.x, there's several breaking changes to be aware of. See the [release notes](https://github.com/ssdukane/WeatherReport/releases/tag/v1.0.0) for details<br />* If you're making the jump from 3.x to 4.x first, there be dragons there too. See [those release notes here](https://github.com/ssdukane/WeatherReport/releases/tag/v1.0.0)|

# Weather Report Service Web API Standards#

* [Guidelines](#guidelines)
* [RESTful URLs](#restful-urls)
* [HTTP Verbs](#http-verbs)
* [Responses](#responses)

A simple and easy to use of weather report service by calling this api end point.

This is simply an alternative interface for the wather report for one or more cities.  

This allows the value to be submitted in a form as well as retreived through normal POST/GET. 

## Guidelines

This document provides guidelines and examples for White House Web APIs, encouraging consistency, maintainability, and best practices across applications. White House APIs aim to balance a truly RESTful API interface with a positive developer experience (DX).



## Pragmatic REST

These guidelines aim to support a truly RESTful API. Here are a few exceptions:
* Put the version number of the API in the URL (see examples below). Don’t accept any requests that do not specify a version number.
* Allow users to request formats like JSON or XML like this:
    * https://localhost:44323/api/weather/
    

## RESTful URLs

### General guidelines for RESTful URLs
* A URL identifies a resource.
* URLs should include nouns, not verbs.
* Use plural nouns only for consistency (no singular nouns).
* Use HTTP verbs (GET, POST, PUT, DELETE) to operate on the collections and elements.
* You shouldn’t need to go deeper than resource/identifier/resource.
* Put the version number at the base of your URL, for example http://example.com/v1/path/to/resource.
* URL v. header:
    * If it changes the logic you write to handle the response, put it in the URL.
    * If it doesn’t change the logic for each response, like OAuth info, put it in the header.
* Specify optional fields in a comma separated list.
* Formats should be in the form of api/v2/resource/{id}.json

### Good URL examples
* Weather of city:
    * GET https://localhost:44323/api/weather/london
* Weather of cities:
    * GET https://localhost:44323/api/weather/london
    * POST https://localhost:44323/api/weather/london
* Weather of city by file upload:
    * POST https://localhost:44323/api/weather

### Bad URL examples
* Non-plural noun:
    * https://localhost:44323/api/weather
* Verb in URL:
    * https://localhost:44323/api/weather/1234/create
* Filter outside of query string
    * https://localhost:44323/api/weather/2011/desc

## HTTP Verbs

HTTP verbs, or methods, should be used in compliance with their definitions under the [HTTP/1.1](http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html) standard.
The action taken on the representation will be contextual to the media type being worked on and its current state. Here's an example of how HTTP verbs map to create, read, update, delete operations in a particular context:

| HTTP METHOD | POST            | GET       | PUT         | DELETE |
| ----------- | --------------- | --------- | ----------- | ------ |
| CRUD OP     | CREATE          | READ      | UPDATE      | DELETE |
| /upload     | Upload list of  | 
|             | in file         |      
| /upload/34  | Error           | 


# How to Improve Weather Service API? #
 
1. Use 

    ```
    https://localhost:44323/api/weather/london
    
1. Install the standard Nuget package into your ASP.NET Core application.

    ```
    Package Manager : Install-Package .AspNetCore
    CLI : dotnet add package Newtonsoft.Json
    CLI : dotnet add package RestSharp
    ```

2. In the `ConfigureServices` method of `Startup.cs`, register the Swagger generator, defining one or more Swagger documents.

    ```csharp
    using Microsoft.OpenApi.Models;
    ```
    
    ```csharp
    services.AddMvc();
    
    ```

3. Ensure your API actions and parameters are decorated with explicit "Http" and "From" bindings.





## Support


I only provide limited support through the Github Issues area. DO NOT ask for support via email, socialmedia, or other means. Also, check the closed issues before opening a new issue.


## Design Methodology 


This project is not meant to be a full featured. I envision this as a weather api that contains a base set of features that can be enhanced using callbacks by the developer. Because of this I am hesitant on adding functionality but am open to adding featuers where it makes sense.

But feel free to open an [issue]
@
(https://github.com/ssdukane/WeatherReport/issues) and suggest a feature requirements
