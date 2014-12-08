using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using XaQiita_Forms;
using XaQiita_iOS;
using CoreGraphics;
using Foundation;
using System.Reflection;

[assembly: ExportRenderer (typeof (GestureListView), typeof (GestureListViewRenderer))]

namespace XaQiita_iOS
{
	public class GestureListViewRenderer : ListViewRenderer
	{
		private UILongPressGestureRecognizer longPressGestureRecognizer;

		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			Action<UILongPressGestureRecognizer> act = (rec) => {
				Console.WriteLine ("long press !!" + rec.View);
				Console.WriteLine ("state:" + rec.State);
				UITableView tv = (UITableView)this.Control;
				CGPoint pt = rec.LocationInView (tv);
				NSIndexPath ip = tv.IndexPathForRowAtPoint (pt);
				var nc = tv.CellAt (ip);
				PropertyInfo pi = nc.GetType ().GetRuntimeProperty ("ViewCell");
				if (pi != null) {
					ViewCell cell = (ViewCell)pi.GetValue (nc);
					Console.WriteLine ("cell:" + cell);
					if (cell is GestureViewCell) {
						MethodInfo mi = cell.GetType ().GetTypeInfo ().GetDeclaredMethod ("DoLongPressEvent");
						if (mi != null) {
							XaQiita_Forms.GestureState state = XaQiita_Forms.GestureState.Unknown;
							switch(rec.State){
							case UIGestureRecognizerState.Began:
								state = XaQiita_Forms.GestureState.Started;
								break;
							case UIGestureRecognizerState.Ended:
								state = XaQiita_Forms.GestureState.Ended;
								break;
							}
							mi.Invoke (cell, new object[]{ state });
						}
					}
				}
			};
			longPressGestureRecognizer = new UILongPressGestureRecognizer (act);

			if (e.NewElement == null) {
				if (longPressGestureRecognizer != null) {
					this.RemoveGestureRecognizer (longPressGestureRecognizer);
				}
			}

			if (e.OldElement == null) {
				this.AddGestureRecognizer (longPressGestureRecognizer);
			}
		}
	}
}

