using System;

namespace XaQiita_Data
{
	public class NetworkingMonitor
	{
		public delegate void NetworkingStatusChangedEventArg(bool isAccessing);

		public static event NetworkingStatusChangedEventArg StatusChanged;

		private static object locker = new object ();
		private static int networkAccessCount = 0;
		public static int NetworkAccessCount {
			get {
				int ret = 0;
				lock (locker) {
					ret = networkAccessCount;
				}
				return ret;
			}
			set {
				int b = networkAccessCount;
				lock (locker) {
					networkAccessCount = value;
				}
				if (b == 0 && networkAccessCount != 0) {
					if (StatusChanged != null) {
						StatusChanged (true);
					}
				}
				if (b != 0 && networkAccessCount == 0) {
					if (StatusChanged != null) {
						StatusChanged (false);
					}
				}
			}
		}
	}
}

