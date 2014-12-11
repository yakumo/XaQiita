using System;
using Xamarin.Forms;
using XaQiita_Localize;

namespace XaQiita_Forms
{
	public class RootPage : MasterDetailPage
	{
		ContentPage rootPage;

		public RootPage ()
		{
			MenuPage mp = new MenuPage () {
				Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0),
			};
			Master = mp;

			rootPage = new ContentPage () {
				Title = Localized.GetString ("ApplicationName"),
			};
			Detail = new NavigationPage (rootPage);

			mp.MenuItems.Add (new MenuItem ("Items", null, new ItemsView()));
			mp.PropertyChanged += (sender, e) => {
				switch (e.PropertyName) {
				case "SelectedMenuItem":
					if (mp.SelectedMenuItem.SelectedAction != null) {
						mp.SelectedMenuItem.SelectedAction.Invoke ();
					}
					if (mp.SelectedMenuItem.SelectedView != null) {
						rootPage.Content = mp.SelectedMenuItem.SelectedView;
					}
					break;
				}
			};
		}
	}
}
