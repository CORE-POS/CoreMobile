using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoreMobileInternals;

namespace CoreMobile
{
	[Activity (Label = "SaleChoices")]			
	public class SaleChoices : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.SaleChoices);

			var btn = FindViewById<Button> (Resource.Id.ownerSaleButton);
			btn.Click += async (sender, e) => {
				Task<List<Product>> getTask = WebService.get_member_sales ();
				List<Product> lp = await getTask;
				string[] data = new string[lp.Count ()];
				for(int i=0; i < lp.Count (); i++){
					data[i] = lp.ElementAt (i).serialize ();
				}
				if (lp.Count > 0){
					var next = new Intent(this, typeof(ProductList));
					next.PutExtra ("productList", data);
					StartActivity (next);
				}
			};

			btn = FindViewById<Button>(Resource.Id.everyoneSaleButton);
			btn.Click += async (sender, e) => {
				Task<List<Product>> getTask = WebService.get_sales ();
				List<Product> lp = await getTask;
				string[] data = new string[lp.Count ()];
				for(int i=0; i < lp.Count (); i++){
					data[i] = lp.ElementAt (i).serialize ();
				}
				if (lp.Count > 0){
					var next = new Intent(this, typeof(ProductList));
					next.PutExtra ("productList", data);
					StartActivity (next);
				}
			};
		}
	}
}

