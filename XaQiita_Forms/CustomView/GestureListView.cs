using System;
using Xamarin.Forms;

namespace XaQiita_Forms
{
	public class GestureListView : ListView
	{
	}

	public enum GestureState {
		Unknown,
		Started,
		Ended
	}

	public class GestureViewCell : ViewCell
	{
		public delegate void LongPressEventArgs(object sender, GestureState state);
		public event LongPressEventArgs LongPress;

		protected void DoLongPressEvent(GestureState state)
		{
			if (LongPress != null) {
				LongPress (this, state);
			}
		}
	}
}

