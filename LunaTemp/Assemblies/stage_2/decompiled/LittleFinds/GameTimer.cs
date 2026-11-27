using System;
using UnityEngine;

namespace LittleFinds
{
	public class GameTimer : MonoBehaviour
	{
		public bool Running;

		public Action Expired;

		public float Remaining { get; private set; } = 120f;


		public void ResetTimer(float seconds = 120f)
		{
			Remaining = seconds;
			Running = true;
		}

		public void Advance(float seconds)
		{
			if (Running)
			{
				Remaining = Mathf.Max(0f, Remaining - seconds);
				if (Remaining == 0f)
				{
					Running = false;
					Expired?.Invoke();
				}
			}
		}

		private void Update()
		{
			Advance(Time.deltaTime);
		}
	}
}
