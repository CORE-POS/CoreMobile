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
	[Activity (Label = "ItemSearchActivity")]			
	public class ItemSearchActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ItemSearch);

			var goBtn = FindViewById<Button> (Resource.Id.ItemSearchButton);
			var searchTerm = FindViewById<EditText> (Resource.Id.ItemSearchTextField);

			goBtn.Click += async (sender, e) => {
				string term = searchTerm.Text;
				if (term == "") return;

				int is_int = 0;
				int.TryParse (term, out is_int);
				Task<List<Product>> tlp = null;
				if (is_int != 0){
					tlp = WebService.get_item(term);
				}
				else {
					tlp = WebService.search_items(term);
				}
				List<Product> lp = await tlp;

				if (lp.Count > 1){
					string[] data = new string[lp.Count ()];
					for(int i=0; i < lp.Count (); i++){
						data[i] = lp.ElementAt (i).serialize ();
					}
					var next = new Intent(this, typeof(ProductList));
					next.PutExtra ("productList", data);
					StartActivity (next);
				}
				else if (lp.Count > 0){
					string data = lp.ElementAt (0).serialize ();
				}
			};
		}
	}
}

