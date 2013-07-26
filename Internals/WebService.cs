using System;
using System.Json;
using System.Net;

using System.IO;

namespace CoreMobileInternals
{
	public class WebService
	{

		public WebService ()
		{
		}

		static public Product get_sales(){
			string url = "http://10.0.2.2:5000/member_sales/";
			DataCache dc = new DataCache ();
			if (dc.get (url) != null)
				return new Product(dc.get (url));

			HttpWebRequest rq = HttpWebRequest.Create (new Uri(url));
			rq.BeginGetResponse ((ar) => {
				HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
				using(HttpWebResponse resp = (HttpWebResponse)request.EndGetResponse (ar)){
					Stream s = resp.GetResponseStream ();
					string val = new StreamReader(s).ReadToEnd ();
					dc.set (url, val);

				}
			}, rq);
		}
	}
}

