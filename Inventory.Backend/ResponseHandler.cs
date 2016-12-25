using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Inventory.Backend {
    public class ResponseHandler : DelegatingHandler {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var response = await base.SendAsync(request, cancellationToken);

            object content;
            string errorMessage = null;
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode) {
                HttpError error = content as HttpError;
                if (error != null) {
                    content = null;
                    errorMessage = error.Message;
                }
            }

            var newResponse = request.CreateResponse(response.StatusCode, new ServiceResponse(response.IsSuccessStatusCode, response.StatusCode, content , errorMessage));
            foreach (var header in response.Headers) {
                newResponse.Headers.Add(header.Key, header.Value);
            }
            return newResponse;
        }
    }
}