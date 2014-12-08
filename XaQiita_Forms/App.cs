using System;
using Xamarin.Forms;

namespace XaQiita_Forms
{
	public class App
	{
		public static Page GetMainPage ()
		{
			return new NavigationPage (new ItemsPage ());
		}
	}
}

