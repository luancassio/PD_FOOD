
namespace PD_FOOD.Infrastructure.Configurations
{
    public class EmailSettingsConfiguration
    {
        public string DefaultFromAddress { get; set; } = string.Empty;
        public string DefaultFromDisplayName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
