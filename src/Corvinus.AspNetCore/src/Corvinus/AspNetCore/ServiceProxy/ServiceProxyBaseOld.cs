// <copyright file="ServiceProxyBaseOld.cs" company="Corvinus Collective">
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
    public class ServiceProxyBaseOld
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProxyBaseOld"/> class.
        /// </summary>
        /// <param name="baseUrl">A string containing the base url of the service.</param>
        public ServiceProxyBaseOld(string baseUrl)
        {
            Client = new HttpClient();
            BaseUrl = new Uri(baseUrl);
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// Gets the BaseUri for the Service.
        /// </summary>
        public Uri BaseUrl { get; }

        /// <summary>
        /// Sends a get statement to the Client and returns the message content as a <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">Return type of the method, should be a string, a byte array, or a stream.</typeparam>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <returns>An object of type T (string, byte[], or Stream) containing the message content.</returns>
        /// <exception cref="ArgumentException">Invalid Type Parameter Type. Generic must be of types <see cref="string"/>, <see cref="byte[]"/>, or <see cref="Stream"/>.</exception>
        /// <exception cref="ArgumentNullException">Url is null or empty.</exception>
        public async Task<T> GetAsync<T>(string url)
        {
            if (typeof(T) != typeof(string) || typeof(T) != typeof(byte[]) || typeof(T) != typeof(Stream))
            {
                throw new ArgumentException("Invalid Type Parameter Type. Generic must be of types string, byte[], or Stream");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url", "This parameter must be a valid subpath from the base url");
            }

            Uri path = new Uri(BaseUrl, url);

            using (HttpResponseMessage message = await Client.GetAsync(path))
            {
                return await ReadMessageAsync<T>(message);
            }
        }

        /*
        /// <summary>
        /// Sends a get statement to the Client and returns the message content as a <see cref="byte[]"/>.
        /// </summary>
        /// <param name="url">A string containing the request url.</param>
        /// <returns>A <see cref="byte[]"/> containing the message content.</returns>
        public async Task<byte[]> GetByteArrayAsync(string url)
        {
            byte[] result = null;
            HttpResponseMessage message = Client.GetAsync(url).Result;
            if (message.IsSuccessStatusCode)
            {
                result = await message.Content.ReadAsByteArrayAsync();
            }

            return result;
        }

        /// <summary>
        /// Sends a get statement to the Client and returns the message content as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="url">A string containing the request url.</param>
        /// <returns>A <see cref="Stream"/> containing the message content.</returns>
        public async Task<Stream> GetStreamAsync(string url)
        {
            Stream result = null;
            HttpResponseMessage message = Client.GetAsync(url).Result;
            if (message.IsSuccessStatusCode)
            {
                result = await message.Content.ReadAsStreamAsync();
            }

            return result;
        }
        */

        /// <summary>
        /// Sends a get statement to the Client and returns the message content as a <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">Return type of the method, should be a string, a byte array, or a stream.</typeparam>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An object of type T (string, byte[], or Stream) containing the message content.</returns>
        /// <exception cref="ArgumentNullException">Url is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task PostAsync(string url, object body)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url", "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body", "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, url);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                await Client.PostAsync(path, content);
                return;
            }
        }

        /// <summary>
        /// Sends a get statement to the Client and returns the message content as a <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">Return type of the method, should be a string, a byte array, or a stream.</typeparam>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>An object of type T (string, byte[], or Stream) containing the message content.</returns>
        /// <exception cref="ArgumentException">Invalid Type Parameter Type. Generic must be of types <see cref="string"/>, <see cref="byte[]"/>, or <see cref="Stream"/>.</exception>
        /// <exception cref="ArgumentNullException">Url is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Body is null.</exception>
        public async Task<T> PostAsync<T>(string url, object body)
        {
            if (typeof(T) != typeof(string) || typeof(T) != typeof(byte[]) || typeof(T) != typeof(Stream))
            {
                throw new ArgumentException("Invalid Type Parameter Type. Generic must be of types string, byte[], or Stream");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url", "This parameter must be a valid subpath from the base url");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body", "This parameter must not be null");
            }

            Uri path = new Uri(BaseUrl, url);

            using (HttpContent content = new StringContent(JsonConvert.SerializeObject(body)))
            {
                using (HttpResponseMessage message = await Client.PostAsync(path, content))
                {
                    return await ReadMessageAsync<T>(message);
                }
            }
        }

        /// <summary>
        /// Sends a post statement to the Client and returs the message content as a <see cref="string"/>.
        /// </summary>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>A <see cref="string"/> containing the message content.</returns>
        public async Task<string> PostStringAsync(string url, object body)
        {
            string result = string.Empty;
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            HttpResponseMessage message = await Client.PostAsync(url, content);

            if (message.IsSuccessStatusCode)
            {
                result = await message.Content.ReadAsStringAsync();
            }

            return result;
        }

        /// <summary>
        /// Sends a post statement to the Client and returs the message content as a <see cref="byte[]"/>.
        /// </summary>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>A <see cref="byte[]"/> containing the message content.</returns>
        public async Task<byte[]> PostByteArrayAsync(string url, object body)
        {
            byte[] result = null;
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            HttpResponseMessage message = await Client.PostAsync(url, content);

            if (message.IsSuccessStatusCode)
            {
                result = await message.Content.ReadAsByteArrayAsync();
            }

            return result;
        }

        /// <summary>
        /// Sends a post statement to the Client and returs the message content as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="url">A <see cref="string"/> containing the request url.</param>
        /// <param name="body">The body object for the post method.</param>
        /// <returns>A <see cref="Stream"/> containing the message content.</returns>
        public async Task<Stream> PostStreamAsync(string url, object body)
        {
            Stream result = null;
            HttpContent content = new StringContent(JsonConvert.SerializeObject(body));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            HttpResponseMessage message = await Client.PostAsync(url, content);

            if (message.IsSuccessStatusCode)
            {
                result = await message.Content.ReadAsStreamAsync();
            }

            return result;
        }

        private async Task<T> ReadMessageAsync<T>(HttpResponseMessage message)
        {
            T result = default;
            if (message.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(string))
                {
                    result = (T)Convert.ChangeType(await message.Content.ReadAsStringAsync(), typeof(T));
                }
                else if (typeof(T) == typeof(byte[]))
                {
                    result = (T)Convert.ChangeType(await message.Content.ReadAsByteArrayAsync(), typeof(T));
                }
                else if (typeof(T) == typeof(Stream))
                {
                    using (Stream stream = await message.Content.ReadAsStreamAsync())
                    {
                        result = (T)Convert.ChangeType(stream, typeof(T));
                    }
                }
            }

            return result;
        }
    }
}
