using System;
using UnityEngine;

namespace LittleFinds
{
	[Serializable]
	public class FindData
	{
		public string title;

		public int category;

		public Sprite art;

		public FindData(string title, int category, Sprite art)
		{
			this.title = title;
			this.category = category;
			this.art = art;
		}
	}
}
