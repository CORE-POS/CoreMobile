using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			btn.Click += (sender, e) => {
				var next = new Intent(this, typeof(ProductList));
				List<Product> lp = Product.test_list ();
				string[] data = new string[lp.Count ()];
				for(int i=0; i < lp.Count (); i++){
					data[i] = lp.ElementAt (i).serialize ();
				}
				next.PutExtra ("productList", data);
				StartActivity (next);
			};

			btn = FindViewById<Button>(Resource.Id.everyoneSaleButton);
			btn.Click += (sender, e) => {
				var next = new Intent(this, typeof(ProductList));
				List<Product> lp = Product.test_list ();
				string[] data = new string[lp.Count ()];
				for(int i=0; i < lp.Count (); i++){
					data[i] = lp.ElementAt (i).serialize ();
				}
				next.PutExtra ("productList", data);
				StartActivity (next);
			};
		}
	}
}

