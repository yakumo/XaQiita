using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace XaQiita_Data
{
	public class QiitaItemsApi : QiitaGetApi<QiitaItems>
	{
		public QiitaItemsApi ()
		{
		}

        public override string UrlPath
        {
            get { return "/api/v2/items"; }
        }

        public override List<KeyValuePair<string, string>> Parameters
        {
            get { return null; }
        }
    }
}

