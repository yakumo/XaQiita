using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XaQiita_Data
{
	[DataContract]
	public class QiitaTag
	{
		[DataMember]
		public string name { get; set; }
		[DataMember]
		public string[] versions { get; set; }
	}

    [CollectionDataContract]
	public class QiitaTags : ObservableCollection<QiitaTag> {}
}
