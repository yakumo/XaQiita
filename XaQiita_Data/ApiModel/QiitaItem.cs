using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XaQiita_Data
{
	[DataContract]
	public class QiitaItem
	{
		[DataMember]
		public string body { get; set; }
		[DataMember]
		public bool coediting { get; set; }
		[DataMember]
		public string created_at { get; set; }
		[DataMember]
		public string id { get; set; }
		[DataMember(Name="private")]
		public string is_private { get; set; }
		[DataMember]
		public QiitaTags tags { get; set; }
		[DataMember]
		public string title { get; set; }
		[DataMember]
		public string updated_at { get; set; }
		[DataMember]
		public QiitaUser user { get; set; }
	}

    [CollectionDataContract]
	public class QiitaItems : ObservableCollection<QiitaItem> {}
}
