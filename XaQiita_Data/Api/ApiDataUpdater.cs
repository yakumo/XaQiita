using System;
using System.Linq;
using System.Threading.Tasks;
using XaQiita_Data;
using System.Diagnostics;

namespace XaQiita_Data
{
	public class ApiDataUpdater
	{
		public static async Task ReloadAll(ModelDispatcher dispatcher)
		{
			NetworkingMonitor.NetworkAccessCount++;

			ApiData apiData = ApiData.Instance;
			QiitaItemsApi itemsApi = new QiitaItemsApi ();
			QiitaItems items = await itemsApi.Call ();
			dispatcher.Invoke (() => {
				foreach (QiitaItem i in items) {
					int idx = (from al in apiData.Items
					           where al.updated_at.CompareTo (i.updated_at) > 0
					           select al).Count ();
					ApiData.Instance.Items.Insert (idx, i);
				}
			});

			NetworkingMonitor.NetworkAccessCount--;
		}

		public static async Task<QiitaItems> ReloadItems(int page){
			NetworkingMonitor.NetworkAccessCount++;

			ApiData apiData = ApiData.Instance;
			QiitaItemsApi itemsApi = new QiitaItemsApi ();
			QiitaItems items = await itemsApi.Call ();

			NetworkingMonitor.NetworkAccessCount--;

			return items;
		}
	}
}

