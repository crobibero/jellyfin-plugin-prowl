using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Plugin.Prowl.Configuration;
using Jellyfin.Plugin.Prowl.Utils;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Notifications;
using MediaBrowser.Model.Serialization;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.Prowl
{
    public class Notifier : INotificationService
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public Notifier(ILoggerFactory loggerFactory, IHttpClient httpClient)
        {
            _logger = loggerFactory.CreateLogger<Notifier>();
            _httpClient = httpClient;
        }

        public bool IsEnabledForUser(User user)
        {
            var options = GetOptions(user);

            return options != null && IsValid(options) && options.Enabled;
        }

        public string Name => Plugin.Instance.Name;

        public async Task SendNotification(UserNotification request, CancellationToken cancellationToken)
        {
            var options = GetOptions(request.User);

            var parameters = new Dictionary<string, string>
            {
                {"apikey", options.Token},
                {"application", "Jellyfin"}
            };

            if (string.IsNullOrEmpty(request.Description))
            {
                parameters.Add("event", request.Name);
            }
            else
            {
                parameters.Add("event", request.Name);
                parameters.Add("description", request.Description);
            }

            _logger.LogDebug("Prowl to {0} - {1} - {2}", options.Token, request.Name, request.Description);

            var url = string.Format(PluginConfiguration.Url, parameters.ToQueryString());
            var requestOptions = new HttpRequestOptions
            {
                Url = url,
                LogErrorResponseBody = true
            };

            await _httpClient.Get(requestOptions).ConfigureAwait(false);
        }

        private static ProwlOptions GetOptions(BaseItem user)
        {
            return Plugin.Instance.Configuration.Options
                .FirstOrDefault(i =>
                    string.Equals(i.UserId, user.Id.ToString("N"), StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsValid(ProwlOptions options)
        {
            return !string.IsNullOrEmpty(options.Token);
        }
    }
}