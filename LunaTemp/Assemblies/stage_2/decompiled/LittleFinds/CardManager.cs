using System.Collections.Generic;
using UnityEngine;

namespace LittleFinds
{
	public class CardManager : MonoBehaviour
	{
		public CardController[] cards;

		public Sprite[] art;

		public GameManager game;

		private static readonly string[] titles = new string[24]
		{
			"Goldfish", "Clownfish", "Blue Tang", "Pufferfish", "Teddy", "Robot", "Duck", "Blocks", "Apple", "Banana",
			"Cherries", "Pear", "Daisy", "Tulip", "Rose", "Bluebell", "Donut", "Cupcake", "Ice Cream", "Candy",
			"Guitar", "Drum", "Piano", "Note"
		};

		public int OpenCount
		{
			get
			{
				int i = 0;
				CardController[] array = cards;
				foreach (CardController c in array)
				{
					if (c.gameObject.activeSelf && c.IsOpen)
					{
						i++;
					}
				}
				return i;
			}
		}

		public bool Collecting
		{
			get
			{
				CardController[] array = cards;
				foreach (CardController c in array)
				{
					if (c.State == CardState.Collecting)
					{
						return true;
					}
				}
				return false;
			}
		}

		public int BoardCount
		{
			get
			{
				int i = 0;
				CardController[] array = cards;
				foreach (CardController c in array)
				{
					if (c.gameObject.activeSelf && c.State != CardState.Removed && c.State != CardState.Collecting)
					{
						i++;
					}
				}
				return i;
			}
		}

		public FindData Item(int index)
		{
			return new FindData(titles[index], index / 4, art[index]);
		}

		public void Deal()
		{
			if ((bool)game.Tutorial && game.Tutorial.InLesson)
			{
				FindData[] lesson = game.Tutorial.BuildDeck();
				for (int i = 0; i < 12; i++)
				{
					cards[i].Bind(game, i, lesson[i]);
				}
				return;
			}
			List<FindData> data = new List<FindData>();
			List<int> needed = new List<int>();
			List<int> remaining = new List<int>();
			int[] targets = game.Collections.Targets;
			foreach (int c in targets)
			{
				if (game.Collections.Count(c) < 4)
				{
					needed.Add(c);
					for (int n2 = game.Collections.Count(c); n2 < 4; n2++)
					{
						remaining.Add(c);
					}
				}
			}
			for (int l = remaining.Count - 1; l > 0; l--)
			{
				int m = Random.Range(0, l + 1);
				int swap = remaining[l];
				remaining[l] = remaining[m];
				remaining[m] = swap;
			}
			int useful = 12 - game.Level.Distractors;
			foreach (int c4 in needed)
			{
				data.Add(Pick(c4));
				remaining.Remove(c4);
			}
			foreach (int c3 in remaining)
			{
				if (data.Count >= useful)
				{
					break;
				}
				data.Add(Pick(c3));
			}
			while (data.Count < useful)
			{
				data.Add(Pick((needed.Count > 0) ? needed[Random.Range(0, needed.Count)] : game.Collections.Targets[0]));
			}
			List<int> decoys = new List<int>(game.Collections.Distractors);
			int[] targets2 = game.Collections.Targets;
			foreach (int c2 in targets2)
			{
				if (game.Collections.Count(c2) >= 4)
				{
					decoys.Add(c2);
				}
			}
			for (int n3 = 0; n3 < game.Level.Distractors; n3++)
			{
				data.Add(Pick((n3 < 2) ? game.Collections.Distractors[n3] : decoys[Random.Range(0, decoys.Count)]));
			}
			for (int k = data.Count - 1; k > 0; k--)
			{
				int n = Random.Range(0, k + 1);
				FindData tmp = data[k];
				data[k] = data[n];
				data[n] = tmp;
			}
			for (int j = 0; j < 12; j++)
			{
				cards[j].Bind(game, j, data[j]);
			}
		}

		private FindData Pick(int c)
		{
			return Item(c * 4 + Random.Range(0, 4));
		}

		public void Freeze()
		{
			CardController[] array = cards;
			foreach (CardController c in array)
			{
				c.Freeze();
			}
		}

		public void RefreshCategories()
		{
			CardController[] array = cards;
			foreach (CardController c in array)
			{
				if (c.Data != null)
				{
					c.RefreshCategoryVisual();
				}
			}
		}
	}
}
