using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace XaQiita_Data
{
	public class ApiData : INotifyPropertyChanged
	{
		private static ApiData instance = null;
		public static ApiData Instance {
			get {
				if (instance == null) {
					instance = new ApiData ();
				}
				return instance;
			}
		}

		private ApiData() {
			Items = new ObservableCollection<QiitaItem> ();
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName) {
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion

		public ObservableCollection<QiitaItem> Items { get; private set; }
	}
}

