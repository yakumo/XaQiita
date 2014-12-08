using System;
using Xamarin.Forms;
using XaQiita_Data;

namespace XaQiita_Forms
{
	public class FormsDispatcher : ModelDispatcher
	{
		#region ModelDispatcher implementation

		public void Invoke (Action action)
		{
			Device.BeginInvokeOnMainThread (action);
		}

		#endregion
	}
}

