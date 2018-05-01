﻿using System.Net.Http;
using System.Text;
using AppGet.CreatePackage.ManifestBuilder;
using AppGet.Http;
using AppGet.Serialization;

namespace AppGet.Manifests.Submission
{
    public class SubmissionResponse
    {
        public string PullRequest { get; set; }
    }

    public interface ISubmissionClient
    {
        SubmissionResponse Submit(PackageManifestBuilder builder);
    }

    public class SubmissionClient : ISubmissionClient
    {
        private readonly IHttpClient _httpClient;

        public SubmissionClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public SubmissionResponse Submit(PackageManifestBuilder builder)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "https://fn.appget.net/api/packages")
            {
                Content = new StringContent(Json.Serialize(builder), Encoding.UTF8, "application/json")
            };

            var resp = _httpClient.Send(req);
            return resp.AsResource<SubmissionResponse>();
        }
    };
}