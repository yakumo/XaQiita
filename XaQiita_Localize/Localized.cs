using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Dynamic;

namespace XaQiita_Localize
{
	public class Localized : DynamicObject
	{
		public static string GetString (string name){
			return GetString (name, "");
		}

		public static string GetString(string name, string comment){
			ResourceManager temp = new ResourceManager ("XaQiita_Localize.Resx.AppResources", typeof(Localized).GetTypeInfo ().Assembly);
			return temp.GetString (name, CultureInfo.DefaultThreadCurrentCulture);
		}

		public override bool TrySetMember (SetMemberBinder binder, object value)
		{
			//return base.TrySetMember (binder, value);
			return true;
		}

		public override bool TryGetMember (GetMemberBinder binder, out object result)
		{
			//return base.TryGetMember (binder, out result);
			string ret = GetString (binder.Name);
			if (ret != null) {
				result = ret;
				return true;
			}
			result = null;
			return false;
		}
	}
}

