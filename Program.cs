// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

Console.WriteLine("Hello, World!");

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddCommandLine(args);

// var config = new Config();
// builder.Configuration.GetSection("Config").Bind(config);

builder.Services.Configure<Config>(builder.Configuration.GetSection(nameof(Config)));
builder.Services.AddHostedService<Worker>();

// Console.WriteLine($"key={config.Key}");
// Console.WriteLine($"username={config.LoginInfo?.Username}");
// Console.WriteLine($"password={config.LoginInfo?.Password}");

var host = builder.Build();

await host.RunAsync();


public class Config
{
    public string Key { get; set; } = string.Empty;
    public LoginInfo? LoginInfo { get; set; }
}

public class LoginInfo
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class Worker : IHostedService
{
    private readonly IOptions<Config> _config;

    public Worker(IOptions<Config> config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Configuration from worker: key={_config.Value.Key} u={_config.Value.LoginInfo?.Username} pw={_config.Value.LoginInfo?.Password}");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}