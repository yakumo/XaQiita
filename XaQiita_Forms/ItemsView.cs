using System;
using System.Linq;
using Xamarin.Forms;
using XaQiita_Localize;
using System.Diagnostics;
using XaQiita_Data;

namespace XaQiita_Forms
{
	public class ItemsView : StackLayout
	{
		private QiitaItems items = new QiitaItems();

		public ItemsView ()
		{
			ListView listView = new GestureListView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,

				ItemTemplate = new DataTemplate (() => {
					Label titleLabel = new Label () {
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.Start,
						LineBreakMode = LineBreakMode.TailTruncation,
						FontSize = 18,
						FontAttributes = FontAttributes.Bold,
					};
					titleLabel.SetBinding<QiitaItem> (Label.TextProperty, i => i.title);

					Label userLabel = new Label () {
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.End,
						FontSize = 12,
					};
					userLabel.SetBinding<QiitaItem> (Label.TextProperty, i => i.user, BindingMode.OneWay, new UserToUserNameConverter ());

					Label tagLabel = new Label () {
						HorizontalOptions = LayoutOptions.EndAndExpand,
						VerticalOptions = LayoutOptions.End,
						LineBreakMode = LineBreakMode.WordWrap,
						FontSize = 7,
						TextColor = Color.Navy,
					};
					tagLabel.SetBinding<QiitaItem> (Label.TextProperty, i => i.tags, BindingMode.OneWay, new TagToTagListStringConverter ());

					Image userImage = new Image () {
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.End,
						MinimumWidthRequest = 16,
						MinimumHeightRequest = 16,
						HeightRequest = 16,
						WidthRequest = 16,
					};
					userImage.SetBinding<QiitaItem> (Image.SourceProperty, i => i.user.profile_image_url);

					var ret = new GestureViewCell () {
						View = new StackLayout () {
							Padding = new Thickness (8, 4),
							Orientation = StackOrientation.Vertical,
							Children = {
								titleLabel,
								new StackLayout () {
									Orientation = StackOrientation.Horizontal,
									Children = {
										userImage,
										userLabel,
										tagLabel,
									},
								},
							},
						},
					};
					ret.Tapped += async (sender, e) => {
						if (sender is ViewCell) {
							ViewCell c = sender as ViewCell;
							if (c.BindingContext != null && c.BindingContext is QiitaItem) {
								QiitaItem qi = c.BindingContext as QiitaItem;
								Debug.WriteLine ("view item, " + qi.id);
								Debug.WriteLine(qi.body);
								var nextPage = new MarkdownPage();
								nextPage.PageTitle = qi.title;
								nextPage.PageBody = qi.body;
								await this.Navigation.PushAsync(nextPage);
							}
						}
					};
					ret.LongPress += (sender, e) => {
						if (e == GestureState.Started) {
							Console.WriteLine ("long press detected !!");
							if (sender is GestureViewCell) {
								GestureViewCell c = sender as GestureViewCell;
								if (c.BindingContext != null && c.BindingContext is QiitaItem) {
									QiitaItem qi = c.BindingContext as QiitaItem;
									Debug.WriteLine ("view item, " + qi.id);
								}
							}
						}
					};
					return ret;
				}),
				ItemsSource = items,
				HasUnevenRows = true,
				RowHeight = 52,
			};

			HorizontalOptions = LayoutOptions.FillAndExpand;
			VerticalOptions = LayoutOptions.FillAndExpand;

			Children.Add (listView);

			ApiDataUpdater.ReloadItems (1).ContinueWith ((res) => {
				new FormsDispatcher ().Invoke(()=>{
					foreach(var i in res.Result){
						items.Add(i);
					}
				});
			});
		}
	}

	public class UserToUserNameConverter : IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			QiitaUser u = (QiitaUser)value;
			if (String.IsNullOrEmpty (u.name)) {
				if(String.IsNullOrEmpty(u.twitter_screen_name)){
					if (String.IsNullOrEmpty (u.github_login_name)) {
						return u.id;
					}
					return u.github_login_name;
				}
				return u.twitter_screen_name;
			}
			return u.name;
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
		#endregion
	}

	public class TagToTagListStringConverter : IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string ret = String.Join (",", (from tag in ((QiitaTags)value) select tag.name));
			return ret;
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return "";
		}
		#endregion
	}
}
