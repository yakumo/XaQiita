using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace XaQiita_Data
{
	[DataContract]
	public class QiitaUser
	{
		[DataMember]
		public string description { get; set; }
		[DataMember]
		public string facebookid { get; set; }
		[DataMember]
		public int followees_count { get; set; }
		[DataMember]
		public int followers_count { get; set; }
		[DataMember]
		public string github_login_name { get; set; }
		[DataMember]
		public string id { get; set; }
		[DataMember]
		public int items_count { get; set; }
		[DataMember]
		public string linkedin_id { get; set; }
		[DataMember]
		public string location { get; set; }
		[DataMember]
		public string name { get; set; }
		[DataMember]
		public string organization { get; set; }
		[DataMember]
		public string profile_image_url { get; set; }
		[DataMember]
		public string twitter_screen_name { get; set; }
		[DataMember]
		public string website_url { get; set; }
	}

    [CollectionDataContract]
	public class QiitaUsers : ObservableCollection<QiitaUser> {}
}
