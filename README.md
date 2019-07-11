
| :mega: Important notice if you're upgrading between major versions! |
|--------------|
|* If you're upgrading from 4.x to 5.x, there's several breaking changes to be aware of. See the [release notes](https://github.com/ssdukane/WeatherReport/releases/tag/v1.0.0) for details<br />* If you're making the jump from 3.x to 4.x first, there be dragons there too. See [those release notes here](https://github.com/ssdukane/WeatherReport/releases/tag/v1.0.0)|

# Weather Report Service #

A simple and easy to use of weather report service by calling this api end point.

This is simply an alternative interface for the wather report for one or more cities.  

This allows the value to be submitted in a form as well as retreived through normal POST/GET. 

# How to? #

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
