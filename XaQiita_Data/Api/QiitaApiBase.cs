using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace XaQiita_Data
{
	public abstract class QiitaApiBase<T>
	{
		public delegate void ResultCallback(object sender, T result);

		public abstract string UrlPath { get; }
		public abstract List<KeyValuePair<string,string>> Parameters { get; }
        public abstract Task<T> Call();
	}

	public abstract class QiitaGetApi<T> : QiitaApiBase<T>
	{
		public override async Task<T> Call()
		{
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(MakeUrl());
            if (res.IsSuccessStatusCode)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                T ret = (T)serializer.ReadObject(await res.Content.ReadAsStreamAsync());
                return ret;
            }
            return default(T);
		}

        private string MakeUrl()
        {
            List<KeyValuePair<string, string>> par = Parameters;

            string url = String.Format("https://qiita.com{0}", UrlPath);
            if (par != null)
            {
                string sep = "?";
                foreach (KeyValuePair<string, string> p in par)
                {
                    url += String.Format("{0}{1}={2}", sep, p.Key, p.Value);
                    sep = "&";
                }
            }
            return url;
        }
    }
}

