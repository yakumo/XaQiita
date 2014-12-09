using System;
using Xamarin.Forms;
using XaQiita_Localize;

namespace XaQiita_Forms
{
	public class RootPage : MasterDetailPage
	{
		public RootPage ()
		{
			Master = new ContentPage () {
				Title = Localized.GetString("Menu"),
				Content = new ContentView(),
			};
			Detail = new NavigationPage (new ItemsPage ());
		}
	}
}

