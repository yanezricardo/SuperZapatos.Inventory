using Inventory.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Inventory.Site.Repositories {
    public class ServiceClientBase {
        protected virtual HttpClient CreateHttpClient(string backendUrl, string mediaType) {
            var client = new HttpClient();
            client.BaseAddress = new Uri(backendUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            string authentication = GetLoginCredentials();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authentication);
            return client;
        }

        private string GetLoginCredentials() {
            string credentials = string.Empty;
            try {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                var value = FormsAuthentication.Decrypt(cookie.Value);
                if (value != null && value.UserData != null) {
                    credentials = value.UserData;
                }
            } catch (Exception) {
            }
            return credentials;
        }
    }
}