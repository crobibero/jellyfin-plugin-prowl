using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.Prowl.Configuration
{
    /// <summary>
    ///     Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public PluginConfiguration()
        {
            Options = new ProwlOptions[] { };
        }

        public ProwlOptions[] Options { get; set; }
        public const string Url = "https://api.prowlapp.com/publicapi/add?{0}";
    }

    public class ProwlOptions
    {
        public bool Enabled { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}