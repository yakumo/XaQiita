using System;
using Xamarin.Forms;
using MarkdownSharp;

namespace XaQiita_Forms
{
	public class MarkdownPage : ContentPage
	{
		private Label titleLabel;
		private WebView bodyView;
		private Markdown markdown = new Markdown();
		private bool isAppearing = false;
		private string _title = "";
		private string _body = "";

		public MarkdownPage ()
		{
			isAppearing = false;
			titleLabel = new Label () {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Start,
				FontSize = 16,
				FontAttributes = FontAttributes.Bold,
				IsVisible = false,
				BackgroundColor=Color.Blue,
			};
			bodyView = new WebView () {
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor=Color.Yellow,
			};
			Content = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Children = {
					titleLabel,
					bodyView,
				},
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			isAppearing = true;
			ApplyTitle ();
			ApplyBody ();
		}

		private void ApplyTitle() {
			titleLabel.Text = _title;
			if (!String.IsNullOrEmpty (_title)) {
				titleLabel.IsVisible = true;
			}
		}

		private void ApplyBody(){
			string html = String.Format ("<html><head></head><body>{0}</body></html>", markdown.Transform (_body));
			Console.WriteLine (html);
			/*
			var htmlSource = new HtmlWebViewSource ();
			htmlSource.Html = html;
			bodyView.Source = htmlSource;
			*/
			var src = new UrlWebViewSource ();
			src.Url = "http://www.xamarin.com/";
			bodyView.Source = src;
		}

		public string PageTitle {
			get {
				return _title;
			}
			set {
				_title = value;
				if (isAppearing) {
					ApplyTitle ();
				}
			}
		}

		public string PageBody {
			get {
				return _body;
			}
			set {
				_body = value;
				if (isAppearing) {
					ApplyBody ();
				}
			}
		}
	}
}
