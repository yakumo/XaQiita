using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using XaQiita_Localize;
using System.ComponentModel;

namespace XaQiita_Forms
{
	public class MenuPage : ContentPage, INotifyPropertyChanged
	{
		private ListView menuListView;

		public MenuPage ()
		{
			MenuItems = new ObservableCollection<MenuItem> ();
			Title = Localized.GetString("Menu");
			menuListView = new ListView () {
				ItemsSource = MenuItems,
				ItemTemplate = new DataTemplate (() => {
					var label = new Label () {
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						FontSize = 16,
						FontAttributes = FontAttributes.Bold,
					};
					label.SetBinding<MenuItem> (Label.TextProperty, mi => mi.LabelText);

					return new ViewCell () {
						View = new StackLayout () {
							Orientation = StackOrientation.Horizontal,
							Children = {
								label,
							},
						},
					};
				}),
			};
			Content = menuListView;
			menuListView.ItemSelected += (sender, e) => {
				OnPropertyChanged("SelectedMenuItem");
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (menuListView.SelectedItem == null) { 
				menuListView.SelectedItem = MenuItems [0];
			}
		}

		public ObservableCollection<MenuItem> MenuItems {
			get;
			set;
		}

		public MenuItem SelectedMenuItem {
			get {
				if (menuListView.SelectedItem == null) {
					return MenuItems [0];
				}
				return (MenuItem)menuListView.SelectedItem;
			}
		}
	}

	public class MenuItem {
		public string LabelText { get; set; }
		public string IconUrl { get; set; }
		public Action SelectedAction { get; private set; }
		public View SelectedView { get; private set; }

		public MenuItem(string label) : this(label, "") {
		}
		public MenuItem(string label, string icon) : this(label, icon, (Action)null) {
		}
		public MenuItem(string label, string icon, Action selectedAction) {
			LabelText = label;
			IconUrl = icon;
			SelectedAction = selectedAction;
			SelectedView = null;
		}
		public MenuItem(string label, string icon, View selectedView) {
			LabelText = label;
			IconUrl = icon;
			SelectedAction = null;
			SelectedView = selectedView;
		}
	}
}

