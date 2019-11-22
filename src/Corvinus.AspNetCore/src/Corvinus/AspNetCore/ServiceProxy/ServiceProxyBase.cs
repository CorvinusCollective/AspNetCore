// <copyright file="ServiceProxyBase.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.ServiceProxy
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// ServiceProxyBase Class.
    /// </summary>
    public class ServiceProxyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProxyBase"/> class.
        /// </summary>
        /// <param name="baseUrl">A string containing the base url of the service.</param>
        public ServiceProxyBase(string baseUrl)
        {
            Client = new HttpClient();
            BaseUrl = new Uri(baseUrl);
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// The BaseUri for the Service.
        /// </summary>
        public Uri BaseUrl { get; }

        /// <summary>
        /// Sends a get request to the Client and returns an object of type T.
        /// </summary>
        /// <typeparam name="T">Return type of the method.</typeparam>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <returns>An object of type T.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        public async Task<T> GetAsync<T>(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpResponseMessage message = await Client.GetAsync(path))
            {
                return await ReadMessageAsync<T>(message);
            }
        }

        /// <summary>
        /// Sends a post request to the Client and returns void.
        /// </summary>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl or the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An awaitable Task.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task PostAsync(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PostAsync(path, content))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a post request to the Client and returns on object of type T.
        /// </summary>
        /// <typeparam name="T">Return type of the method.</typeparam>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An object of type T.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task<T> PostAsync<T>(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PostAsync(path, content))
                {
                    return await ReadMessageAsync<T>(message);
                }
            }
        }

        /// <summary>
        /// Sends a put request to the Client and returns void.
        /// </summary>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An awaitable Task.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task PutAsync(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PutAsync(path, content))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a put request to the Client and returns object of type T.
        /// </summary>
        /// <typeparam name="T">Return type of the method.</typeparam>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An object of type T.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task<T> PutAsync<T>(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PutAsync(path, content))
                {
                    return await ReadMessageAsync<T>(message);
                }
            }
        }

        /// <summary>
        /// Sends a patch request to the Client and returns void.
        /// </summary>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An awaitable Task.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task PatchAsync(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PatchAsync(path, content))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a put request to the Client and returns object of type T.
        /// </summary>
        /// <typeparam name="T">Return type of the method.</typeparam>
        /// <param name="relativeUrl">A <see cref="string"/> containing the relativeUrl of the request.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An object of type T.</returns>
        /// <exception cref="ArgumentNullException">relativeUrl is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task<T> PatchAsync<T>(string relativeUrl, object body)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                throw new ArgumentNullException(nameof(relativeUrl), "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, relativeUrl);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PatchAsync(path, content))
                {
                    return await ReadMessageAsync<T>(message);
                }
            }
        }

        private async Task<T> ReadMessageAsync<T>(HttpResponseMessage message)
        {
            T result = default;
            if (message.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
            }

            return result;
        }
    }
}
