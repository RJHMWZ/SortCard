using System;
using UnityEngine;

namespace LittleFinds
{
	public class CollectionManager : MonoBehaviour
	{
		public RectTransform[] pools;

		public CollectionBox[] boxes;

		private readonly int[] counts = new int[6];

		private readonly int[] reserved = new int[6];

		public int[] Targets { get; private set; } = new int[3] { 0, 1, 2 };


		public int[] Distractors { get; private set; } = new int[2] { 3, 4 };


		public int Total
		{
			get
			{
				int total = 0;
				int[] targets = Targets;
				foreach (int c in targets)
				{
					total += counts[c];
				}
				return total;
			}
		}

		public int Slot(int c)
		{
			return Array.IndexOf(Targets, c);
		}

		public bool IsTarget(int c)
		{
			return Slot(c) >= 0;
		}

		public CollectionBox BoxFor(int c)
		{
			return boxes[Slot(c)];
		}

		public int Count(int c)
		{
			return (c >= 0 && c < 6) ? counts[c] : 0;
		}

		public bool IsSatisfiedOrReserved(int c)
		{
			return !IsTarget(c) || counts[c] + reserved[c] >= 4;
		}

		public bool CanReserve(int c)
		{
			return IsTarget(c) && counts[c] + reserved[c] < 4;
		}

		public bool Reserve(int c)
		{
			if (!CanReserve(c))
			{
				return false;
			}
			reserved[c]++;
			return true;
		}

		public void Commit(int c, Sprite item)
		{
			if (IsTarget(c) && reserved[c] > 0)
			{
				reserved[c]--;
				counts[c]++;
				BoxFor(c).Accept(item, counts[c]);
			}
		}

		public void ResetCollections()
		{
			GameManager game = GetComponent<GameManager>();
			int[] order = new int[6] { 0, 1, 2, 3, 4, 5 };
			if (!game.Tutorial.InLesson)
			{
				int[] targets = game.Level.Targets;
				int index = 0;
				int[] array = targets;
				foreach (int c3 in array)
				{
					order[index++] = c3;
				}
				for (int c4 = 0; c4 < 6; c4++)
				{
					if (Array.IndexOf(targets, c4) < 0)
					{
						order[index++] = c4;
					}
				}
			}
			Targets = new int[3]
			{
				order[0],
				order[1],
				order[2]
			};
			Distractors = new int[2]
			{
				order[3],
				order[4]
			};
			for (int c2 = 0; c2 < 6; c2++)
			{
				counts[c2] = (reserved[c2] = 0);
			}
			for (int slot = 0; slot < 3; slot++)
			{
				int c = Targets[slot];
				boxes[slot].Configure(c, game.Cards.art[c * 4]);
				boxes[slot].ResetBox();
			}
		}

		public void CancelReservations()
		{
			for (int c = 0; c < 6; c++)
			{
				reserved[c] = 0;
			}
		}
	}
}
