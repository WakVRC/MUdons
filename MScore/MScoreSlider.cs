
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class MScoreSlider : UdonSharpBehaviour
	{
		[SerializeField] private Slider slider;
		[SerializeField] private MScore mScore;

		private void Update()
		{
			int newScore = (int)(slider.value * mScore.maxScore);
			if (mScore.Score != newScore)
				mScore.SetScore(newScore);
		}

		public void Init()
		{
			slider.value = 0;
		}
	}
}