igloo15.Serilog.Microsoft.Logger
===
A not pre release package for extending Microsoft.Extensions.Logging with the latest Serilog Console and File Sinks

## Packages

[![Nuget](https://img.shields.io/nuget/dt/Cake.igloo15.Scripts.NuGet.svg?label=igloo15.Serilog.Microsoft.Logging.File)](https://www.nuget.org/packages/igloo15.Serilog.Microsoft.Logging.File/)
[![Nuget](https://img.shields.io/nuget/dt/Cake.igloo15.Scripts.Changelog.svg?label=igloo15.Serilog.Microsoft.Logging.Console)](https://www.nuget.org/packages/igloo15.Serilog.Microsoft.Logging.Console/)
[![Nuget](https://img.shields.io/nuget/dt/Cake.igloo15.Scripts.Markdown.svg?label=igloo15.Serilog.Microsoft.Logging.Core)](https://www.nuget.org/packages/igloo15.Serilog.Microsoft.Logging.Core/)

## Install

```
dotnet add package igloo15.Serilog.Microsoft.Logging.File
dotnet add package igloo15.Serilog.Microsoft.Logging.Console
```

## Usage

### Configure File Logging

```csharp
LoggerFactory factory = new LoggerFactory();

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var section = config.GetSection("Logging:File");
var consoleSection = config.GetSection("Logging:SerilogConsole");

factory.AddFile(section);
factory.AddSerilogConsole(consoleSection);

```

```csharp
LoggerFactory factory = new LoggerFactory();

var config = new FileConfiguration();
var consoleConfig = new ConsoleConfiguration();

factory.AddFile(config);
factory.AddSerilogConsole(consoleConfig);

```

```csharp
public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logBuilder) =>
                {
					logBuilder.AddFile(context.Configuration.GetSection("Logging:File"));
					logBuilder.AddSerilogConsole(context.Configuration.GetSection("Logging:SerilogConsole"));
                })
                .UseStartup<Startup>();
```

```csharp
public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logBuilder =>
                {
					logBuilder.AddFile(new FileConfiguration());
					logBuilder.AddSerilogConsole(new ConsoleConfiguration());
                })
                .UseStartup<Startup>();
```

```csharp
public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logBuilder =>
                {
					logBuilder.AddConfiguration(config.GetSection("Logging"));
					logBuilder.AddFile();
					logBuilder.AddSerilogConsole();
                })
                .UseStartup<Startup>();
```

#### File Configuration

```json
{
  "Logging": {
    "File": {
      "LogLevel": {
        "Default": "Warning"
      },
      "Shared": false,
      "Template": "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}",
      "FlushInterval": 2,
      "PathFormat": "App.log",
      "FileCountLimit": 31,
      "FileSizeLimitMegaBytes": 10737.41824,
      "FileSizeLimit": 1073741824,
      "IncludeScopes": true,
      "RenderJson": false,
      "RollingInterval": "Hour",
      "GroupLogging": true,
      "AsyncBufferSize": 10000,
      "DropLogsOnBufferLimit": false
    }
  }
}

```

#### Console Configuration

```json
{
  "Logging": {
    "SerilogConsole": {
      "LogLevel": {
        "Default": "Information"
      },
      "Theme": "SystemConsoleThemeLiterate",
      "IncludeScopes": true,
      "Template": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
      "AsyncBufferSize": 10000,
      "DropLogsOnBufferLimit": false
    }
  }
}

```
