using System;
using System.Collections.Generic;

namespace CoreMobileInternals
{
	[Serializable]
	public class Product
	{
		private string upc;
		public string UPC {
			get { return upc; }
			set {
				upc = value.PadLeft (13, '0');
			}
		}

		public string description { get; set; }
		public string brand { get; set; }
		public bool scale { get; set; }
		public double normal_price { get; set; }
		public double sale_price { get; set; }
		public double member_price { get; set; }

		public Product ()
		{
			description = "";
			brand = "";
			scale = false;
			normal_price = 0.00;
			sale_price = 0.00;
			member_price = 0.00;
			upc = null;
		}

		/**
		 * Deserialize constructor
		 */
		public Product(string serialized){
			foreach (string s in serialized.Split (new string[]{"\r\r"}, StringSplitOptions.None)) {
				switch (s.Substring (0, 4)) {
				case "__up":
					upc = s.Substring (4);
					break;
				case "__br":
					brand = s.Substring (4);
					break;
				case "__de":
					description = s.Substring (4);
					break;
				case "__sc":
					scale = (s.Substring (4) == "1") ? true : false;
					break;
				case "__np":
					normal_price = double.Parse (s.Substring (4));
					break;
				case "__sp":
					sale_price = double.Parse (s.Substring (4));
					break;
				case "__mp":
					member_price = double.Parse (s.Substring (4));
					break;
				}
			}
		}

		public string serialize(){
			string output = "";
			output += "__up" + upc + "\r\r";
			output += "__br" + brand + "\r\r";
			output += "__de" + description + "\r\r";
			output += "__sc" + (scale ? 1 : 0) + "\r\r";
			output += "__np" + normal_price + "\r\r";
			output += "__sp" + sale_price + "\r\r";
			output += "__mp" + member_price;
			return output;
		}

		/**
		 * Generate sample item for testing
		 */
		public static Product test_instance(bool on_sale=false, bool mem_sale=false){
			var p = new Product ();
			p.UPC = "123";
			p.description = "Testing item";
			p.brand = "Fake Brand";
			p.normal_price = 5.95;
			if (on_sale) p.sale_price = 5.55;
			else if (mem_sale) p.member_price = 5.55;
			return p;
		}

		/**
		 * Generate List of sample items for testing
		 */
		public static List<Product> test_list(){
			List<Product> plist = new List<Product> ();
			plist.Add (Product.test_instance ());
			plist.Add (Product.test_instance (true));
			plist.Add (Product.test_instance (false, true));
			return plist;
		}
	}
}

