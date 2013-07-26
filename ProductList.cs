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
	[Activity (Label = "ProductList")]			
	public class ProductList : Activity
	{
		private List<Product> plist;
		ListView listView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			string[] data = Intent.GetStringArrayExtra ("productList");
			plist = new List<Product> ();
			foreach (string d in data) {
				plist.Add (new Product (d));
			}

			// Create your application here
			SetContentView (Resource.Layout.ProductList);

			listView = FindViewById<ListView> (Resource.Id.productListView);
			listView.Adapter = new ProductListAdapter (this, plist);
		}
	}

	public class ProductListAdapter : BaseAdapter<Product> {
		List<Product> items;
		Activity context;
		public ProductListAdapter(Activity ctxt, List<Product> pl) : base(){
			items = pl;
			context = ctxt;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Product this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.ProductListItem, null);
			view.FindViewById<TextView>(Resource.Id.PListDescription).Text = item.description;
			if (Math.Abs (item.sale_price) > 0.005) {
				view.FindViewById<TextView> (Resource.Id.PListPrice).Text = item.sale_price.ToString ();
				view.FindViewById<TextView> (Resource.Id.PListOther).Text = item.brand + "  -  On Sale";
			} else if (Math.Abs (item.member_price) > 0.005) {
				view.FindViewById<TextView> (Resource.Id.PListPrice).Text = item.member_price.ToString ();
				view.FindViewById<TextView> (Resource.Id.PListOther).Text = item.brand + "  -  Member Sale";
			} else {
				view.FindViewById<TextView> (Resource.Id.PListPrice).Text = item.normal_price.ToString ();
				view.FindViewById<TextView> (Resource.Id.PListOther).Text = item.brand;
			}
			return view;
		}
	}
}

