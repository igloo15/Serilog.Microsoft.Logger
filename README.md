igloo15.Serilog.Microsoft.Logger
===
A not pre release package for extending Microsoft.Extensions.Logging with the latest Serilog Console and File Sinks

## Install

```
dotnet add package igloo15.Serilog.Microsoft.Logger
```

## Usage

### Configure File Logging

```csharp
LoggerFactory factory = new LoggerFactory();

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var section = config.GetSection("Logging:File");

factory.AddFile(section);

```

```csharp
LoggerFactory factory = new LoggerFactory();

var config = new FileConfiguration();

factory.AddFile(config);

```

```csharp
public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logBuilder) =>
                {
					logBuilder.AddFile(context.Configuration.GetSection("Logging:File"));
                })
                .UseStartup<Startup>();
```

```csharp
public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logBuilder =>
                {
					l.AddFile(new FileConfiguration());
                })
                .UseStartup<Startup>();
```

Json Configuration

```json
{
  "Logging": {
    "File": {
      "LogLevel": {
        "Default": "Information"
      },
      "Shared": false,
      "Template": "{Timestamp:o} {RequestId,13} [{Level:u3}] {Message} ({EventId:x8}){NewLine}{Exception}",
      "FlushInterval": 2,
      "PathFormat": "Logs/MyApp.log",
      "FileCountLimit": 31,
	  "FileSizeLimit": 1073741824,
      "IncludeScopes": false,
	  "BufferSize": null
    }
  }
}

```