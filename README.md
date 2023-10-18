# dotnet-command-line
Demo app for testing configuration values when input from a command-line using Microsoft.Extensions.Configuration.CommandLine.

To run this app and test passing in command-line parameters, run the following command

`dotnet run Config:Key="secret key" Config:LoginInfo:Username="someuser" Config:LoginInfo:Password="somepw"`

The colon separators in the keys help map the values to the following class structure.

```C#
public class Config
{
    public string Key { get; set; } = string.Empty;
    public LoginInfo? LoginInfo { get; set; }
}
```
