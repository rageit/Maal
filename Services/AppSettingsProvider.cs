namespace Maal.Services;
public interface IAppSettingsProvider
{
    public string DbPath { get; }
}
public class AppSettingsProvider : IAppSettingsProvider
{
    private readonly IConfiguration _configuration;

    public AppSettingsProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string DbPath => _configuration["DbPath"] ?? "maal.db";
}
