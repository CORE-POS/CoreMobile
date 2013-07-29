using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Json;

namespace CoreMobile
{
	[Activity (Label = "CoreMobile", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var salesBtn = FindViewById<Button> (Resource.Id.salesButton);
			salesBtn.Click += (sender, e) => {
				StartActivity (typeof(SaleChoices));
			};

			var searchBtn = FindViewById<Button> (Resource.Id.searchButton);
			searchBtn.Click += (sender, e) => {
				StartActivity (typeof(ItemSearchActivity));
			};
		}
	}
}


