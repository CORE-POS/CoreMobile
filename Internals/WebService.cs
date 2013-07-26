using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.IO;

namespace CoreMobileInternals
{
	public class WebService
	{

		public WebService ()
		{
		}

		static public async Task<List<Product>> get_sales(){
			string url = "http://10.0.2.2:5000/member_sales/";
			DataCache dc = new DataCache ();
			if (dc.get (url) != null)
				return Product.listFromJson (dc.get (url));

			string data = await WebService.stringFromUrl (new Uri(url));
			dc.set (url, data);
		
			return Product.listFromJson (data);
		}

		static private async Task<string> stringFromUrl(Uri url){
			var request = (HttpWebRequest)HttpWebRequest.Create (url);
			WebResponse resp = await request.GetResponseAsync ();
			Stream s = resp.GetResponseStream ();
			return new StreamReader(s).ReadToEnd ();
		}
	}
}

