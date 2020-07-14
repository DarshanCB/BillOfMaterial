using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace BOM_Importer_V2
{
    class ArasInterface
    {

        private string innovatorUrl = "";
        private string innovatorUsername = "";
        private string innovatorPassword = "";
        private string innovatorDatabase = "";
        const string oauthServerClientId = "IOMApp1"; // must be registered in authorization server's oauth.config file


        public ArasInterface()
        {
            innovatorUrl = Properties.Settings.Default.ARAS_URL.ToString();
            innovatorUsername = Properties.Settings.Default.ARAS_DB_Username.ToString();
            innovatorPassword = GetMD5Hash(Properties.Settings.Default.ARAS_DB_Userpassword);
            innovatorDatabase = Properties.Settings.Default.ARAS_DB_Name.ToString();

        }



        public string GetToken()
        {
            Log.Write("GetToken");
            string innovatorServerDiscoveryUrl = innovatorUrl + "Server/OAuthServerDiscovery.aspx";

            string oauthServerUrl = GetOAuthServerUrl(innovatorServerDiscoveryUrl);

            Log.Write("oauthServerUrl = " + oauthServerUrl);

            if (oauthServerUrl == null)
                return "";

            //
            // Get token endpoint
            // ==================
            var oauthServerConfigurationUrl = oauthServerUrl + ".well-known/openid-configuration";

            var tokenUrl = GetTokenEndpoint(oauthServerConfigurationUrl);

            Log.Write("tokenUrl = " + tokenUrl);

            if (tokenUrl == null)
                return "";


            // Get access token
            // ================
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("scope", "Innovator"),
                new KeyValuePair<string, string>("client_id", oauthServerClientId),
                new KeyValuePair<string, string>("username", innovatorUsername),
                new KeyValuePair<string, string>("password", innovatorPassword),
                new KeyValuePair<string, string>("database", innovatorDatabase),
            });

            var tokenData = GetJson(tokenUrl, body);

            if (tokenData == null)
                return "";


            //
            // Request parts using OData
            // =========================
            var result = JObject.Parse(tokenData);
            string access_token = (string)result["access_token"];

            Log.Write("got token " + access_token);

            return access_token;
        }


        private string GetOAuthServerUrl(string url)
        {
            var discovery = GetJson(url);
            var result = JObject.Parse(discovery);
            var values = (JArray)result["locations"];
            string uri = string.Empty;

            foreach (var value in values)
            {
                uri = (string)value["uri"];

            }

            return uri;
        }

        private string GetTokenEndpoint(string url)
        {
            var configuration = GetJson(url);
            var result = JObject.Parse(configuration);
            var token_endpoint = (string)result["token_endpoint"];

            return token_endpoint;
        }


        private dynamic GetJson(string url, HttpContent body = null)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                if (body == null)
                {
                    response = client.GetAsync(url).Result;
                }
                else
                {
                    response = client.PostAsync(url, body).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.RequestMessage.Content.ReadAsStringAsync().Result;
                }

            }

        }

        private string GetMD5Hash(string input)
        {

            //MD5Hash - calculation does not work well
            //var data = System.Text.Encoding.ASCII.GetBytes(input);
            //data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            //return Convert.ToBase64String(data);


            //!!!!!!!!!!!!!!!!!!!!!!!!!!
            // need rework!!!

            // MD5 md5 = new MD5CryptoServiceProvider();
            // byte[] textToHash = Encoding.Default.GetBytes(input);
            // byte[] result = md5.ComputeHash(textToHash);


            //Console.WriteLine("###" + System.BitConverter.ToString(result));


            // return System.BitConverter.ToString(result);

            return MD5Hash(input);
        }

        private string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();


        }

        public HttpResponseMessage GET_Response(string urlParam, string token)
        {
            string req_url = innovatorUrl + "server/odata/" + urlParam;

            // GET request is executed
            HttpResponseMessage response = Request_Result(req_url, "GET", token, null);

            return response;
        }

        public HttpResponseMessage POST_Response(object req, string urlParam, string token, string sTarget = "server", Dictionary<string, string> additionalHeaders = null, string application = "json")
        {


            //string req_url = innovatorUrl + "server/odata/" + urlParam;
            string req_url = innovatorUrl + sTarget + "/odata/" + urlParam;

            var encodedContent = JsonConvert.SerializeObject(req);
            HttpContent Content = new StringContent(encodedContent, UnicodeEncoding.UTF8, "application/json");

            // POST request is executed
            HttpResponseMessage response = Request_Result(req_url, "POST", token, Content, additionalHeaders, application);

            return response;

        }

        public HttpResponseMessage PATCH_Response(object req, string urlParam, string token)
        {

            string req_url = innovatorUrl + "server/odata/" + urlParam;

            var encodedContent = JsonConvert.SerializeObject(req);
            HttpContent Content = new StringContent(encodedContent, UnicodeEncoding.UTF8, "application/json");

            // PATCH request is executed
            HttpResponseMessage response = Request_Result(req_url, "PATCH", token, Content);

            return response;

        }


        public HttpResponseMessage DELETE_Response(string urlParam, string token)
        {

            string req_url = innovatorUrl + "server/odata/" + urlParam;

            
            // DELETE request is executed
            HttpResponseMessage response = Request_Result(req_url, "DELETE", token, null);

            return response;

        }

        public HttpResponseMessage Request_Result(string url, string request, string accessToken = null, HttpContent content = null, Dictionary<string, string>  additionalHeaders = null, string application = "json")
        {
            using (HttpClient client = new HttpClient())
            {

                //application could be json, octet-stream ...
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/"+application));

                if (accessToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                // additional headers
                if (additionalHeaders != null)
                {
                    foreach (KeyValuePair<string, string> kvp in additionalHeaders)
                    {
                        string sKey = kvp.Key;
                        string sValue = kvp.Value;

                        client.DefaultRequestHeaders.Add(sKey,sValue );
                    }
                }

                HttpResponseMessage response = null;
                if (request == "GET")
                {
                    response = client.GetAsync(url).Result;
                }
                else if (request == "POST")
                {
                    response = client.PostAsync(url, content).Result;
                }
                else if(request == "PUT")
                {
                    response = client.PutAsync(url, content).Result;
                }
                else if (request == "PATCH")
                {
                    response = client.PatchAsync(url, content).Result;
                }
                else if (request == "DELETE")
                {
                    response = client.DeleteAsync(url).Result;
                }
                return response;
            }
        }
    }



    //extends HttpClient class with PatchAsync
    public static class HttpClientExension
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {

            Uri uri = new Uri(requestUri);

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content
            };

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException ex)
            {
                Log.Write("exception in PatchAsync " + ex.ToString());
            }
            return response;
        }
    }
}
#region oldstuff

//========================================================================================================================
// just as a reminder

namespace oldstuff
{
    class ArasInterface_old
    {

        /*
        string Invoke(string Method, string Uri,  string Body  )
        {
            var cl = new HttpClient();
            cl.BaseAddress = new Uri(Uri);
            int _TimeoutSec = 90;
            cl.Timeout = new TimeSpan(0, 0, _TimeoutSec);
            string _ContentType = "application/json";
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
            var _CredentialBase64 = "RWRnYXJTY2huaXR0ZW5maXR0aWNoOlJvY2taeno=";
            cl.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", _CredentialBase64));
            var _UserAgent = "d-fens HttpClient";
            // You can actually also set the User-Agent via a built-in property
            cl.DefaultRequestHeaders.Add("User-Agent", _UserAgent);
            // You get the following exception when trying to set the "Content-Type" header like this:
            // cl.DefaultRequestHeaders.Add("Content-Type", _ContentType);
            // "Misused header name. Make sure request headers are used with HttpRequestMessage, response headers with HttpResponseMessage, and content headers with HttpContent objects."

            HttpResponseMessage response;
            var _Method = new HttpMethod(Method);

            switch (_Method.ToString().ToUpper())
            {
                case "GET":
                case "HEAD":
                    // synchronous request without the need for .ContinueWith() or await
                    response = cl.GetAsync(Uri).Result;
                    break;
                case "POST":
                    {
                        // Construct an HttpContent from a StringContent
                        HttpContent _Body = new StringContent(Body);
                        // and add the header to this object instance
                        // optional: add a formatter option to it as well
                        _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
                        // synchronous request without the need for .ContinueWith() or await
                        response = cl.PostAsync(Uri, _Body).Result;
                    }
                    break;
                case "PUT":
                    {
                        // Construct an HttpContent from a StringContent
                        HttpContent _Body = new StringContent(Body);
                        // and add the header to this object instance
                        // optional: add a formatter option to it as well
                        _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
                        // synchronous request without the need for .ContinueWith() or await
                        response = cl.PutAsync(Uri, _Body).Result;
                    }
                    break;
                case "DELETE":
                    response = cl.DeleteAsync(Uri).Result;
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            // either this - or check the status to retrieve more information
            response.EnsureSuccessStatusCode();
            // get the rest/content of the response in a synchronous way
            var content = response.Content.ReadAsStringAsync().Result;

            return content;
        }

    */

        public void Request()
        {
            Task t = new Task(RequestRead);
            t.Start();
        }

        static public async void RequestRead()
        {

            //===============================================================================
            //Attention: here we just request data for part "900453"
            //this is only a proof of concept!!
            //===============================================================================
            const string URL = "http://pdm-test.elcon-system.de/Innovator11/";
            string urlParameters = "server/odata/Part?$filter=item_number eq '900453'";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            string _ContentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));

            client.DefaultRequestHeaders.Add("DATABASE", "InnovatorSolutions11");
            client.DefaultRequestHeaders.Add("AUTHUSER", "rene");
            client.DefaultRequestHeaders.Add("AUTHPASSWORD", "93de1a7f9a00f8823ac377738b66236b");

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {

                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();

                    // ... Display the result.
                    if (result != null && result.Length >= 150)
                    {
                        Console.WriteLine(result.Substring(0, 150) + "...");

                        object resultObj = JsonConvert.DeserializeObject<object>(result);

                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();
        }
    }
}

#endregion