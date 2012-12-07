using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using WebKit;

namespace _3322
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string ip;
        private void Form1_Load(object sender, EventArgs e)
        {
            ip = GetClientInternetIP();
            label1.Text += ip;


        }
















        /// <summary>
        /// 获得客户端外网IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static string GetClientInternetIP()
        {
            string ipAddress = "";
            using (WebClient webClient = new WebClient())
            {
                ipAddress = webClient.DownloadString("http://ip.changeip.com/");//站获得IP的网页
                //判断IP是否合法
                if (!System.Text.RegularExpressions.Regex.IsMatch(ipAddress, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
                {
                    ipAddress = webClient.DownloadString("http://ip.changeip.com/");//站获得IP的网页
                }
            }

            string[] sArray = ipAddress.Split('=');
            string ip1 = sArray[1].ToString();
            ipAddress = ip1.Substring(0, ip1.Length - 5);
            return ipAddress;
        }


        /// <summary>      /// 有关HTTP请求的辅助类      /// </summary>     
        public class HttpWebResponseUtility
        {
            private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            /// <summary>      
            /// 创建GET方式的HTTP请求
            /// </summary>        
            /// <param name="url">请求的URL</param> 
            /// <param name="timeout">请求的超时时间</param>  
            /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
            /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
            /// <returns></returns> 
            public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
            {
                if (string.IsNullOrEmpty(url)) { throw new ArgumentNullException("url"); }
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = DefaultUserAgent;
                if (!string.IsNullOrEmpty(userAgent))
                {
                    request.UserAgent = userAgent;
                }
                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value;
                }
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                return request.GetResponse() as HttpWebResponse;
            }

            /// <summary>          
            /// 创建POST方式的HTTP请求 
            /// </summary>
            /// <param name="url">请求的URL</param>
            /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
            /// <param name="timeout">请求的超时时间</param>
            /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param> 
            /// <param name="requestEncoding">发送HTTP请求时所用的编码</param> 
            /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
            /// <returns></returns>
            public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException("url");
                } if (requestEncoding == null)
                {
                    throw new ArgumentNullException("requestEncoding");
                } HttpWebRequest request = null;              //如果是发送HTTPS请求            
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                if (!string.IsNullOrEmpty(userAgent))
                {
                    request.UserAgent = userAgent;
                }
                else
                {
                    request.UserAgent = DefaultUserAgent;
                }
                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value;
                } if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }              //如果需要POST数据             
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    byte[] data = requestEncoding.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                } return request.GetResponse() as HttpWebResponse;
            }
            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true; //总是接受         
            }
        }

       WebKitBrowser aa = new WebKitBrowser();
        
        private void button1_Click(object sender, EventArgs e)
        {
            //    if (textBox1.Text ==""  || textBox2.Text== "")

            //    {
            //        MessageBox.Show("必填內容不能为空！");

            //    }

            ////



            //string loginUrl = "http://members.3322.org/dyndns/update?system=dyndns&hostname=dg360.8800.org&myip=" + ip + "&wildcard=OFF";
            //string userName = "fromdg360";
            ////byte[] bytes = Encoding.Default.GetBytes("1234566");
            ////string pass = Convert.ToBase64String(bytes);
            //string password = "1234566";
            //IDictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("Host:", "members.3322.net");
            //parameters.Add("Authorization:", userName+":"+password);
            ////Authorization: Basic username:password

          
            //HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);

            /////
            /*browser = browserControl;
    
            browser.Visible = true;
            browser.Dock = DockStyle.Fill;
            browser.Name = "browser";
            //browser.IsWebBrowserContextMenuEnabled = false;
            //browser.IsScriptingEnabled = false;
            container.ContentPanel.Controls.Add(browser);

            // context menu

            this.Controls.Add(container);
            this.Text = "<New Tab>";

            // events
            browser.DocumentTitleChanged += (s, e) => this.Text = browser.DocumentTitle;
            browser.Navigating += (s, e) => statusLabel.Text = "Loading...";
            browser.Navigated += (s, e) => { statusLabel.Text = "Downloading..."; };
            browser.DocumentCompleted += (s, e) => { statusLabel.Text = "Done"; };
            if (goHome)
                browser.Navigate("http://www.google.com");



            */





            ////string str;
            ////str = "http://members.3322.org/dyndns/update?system=dyndns&hostname=dg360.3322.org&myip=" + ip + "&wildcard=OFF&"+textBox1.Text + ":"+ pass;
            ////string ss;
            ////using (WebClient webClient = new WebClient())
            ////{
            ////    ss = webClient.DownloadString(str);
            ////}
            //label2.Text = response.ToString();


            string url = "http://members.3322.net/dyndns/update?system=dyndns&hostname=dg360.8800.org&myip=192.168.39.198";

            try
            {
                 aa.Navigate(url);

            }
            catch (WebException ee)
            {

                MessageBox.Show(ee.Message.ToString());
            }

            finally
            {

                label2.Text = aa.DocumentText;

            
            }

        }




    }
}
