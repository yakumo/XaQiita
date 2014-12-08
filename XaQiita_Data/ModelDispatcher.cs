using System;

namespace XaQiita_Data
{
	public interface ModelDispatcher
	{
		void Invoke(Action action);
	}
}
