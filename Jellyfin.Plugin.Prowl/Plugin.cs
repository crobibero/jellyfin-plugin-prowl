using System;
using System.Collections.Generic;
using Jellyfin.Plugin.Prowl.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.Prowl
{
    /// <summary>
    ///     Class Plugin
    /// </summary>
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        private readonly Guid _id = new Guid("E33266B6-4E16-4412-8C2C-A93D65D458E5");

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override Guid Id => _id;

        /// <summary>
        ///     Gets the name of the plugin
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Prowl";

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description => "Sends notifications via Prowl Service.";
        
        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = Name,
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.config.html"
                }
            };
        }
    }
}