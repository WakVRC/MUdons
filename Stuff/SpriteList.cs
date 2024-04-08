using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Mascari4615
{
	// [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class SpriteList : MBase
	{
		[Header("_" + nameof(SpriteList))]
		[SerializeField] private MScore mScore;
		[SerializeField] private Sprite[] spriteList;
		[SerializeField] private Image targetImage;
		[SerializeField] private SpriteRenderer targetSpriteRenderer;

		public void UpdateSpriteByScore()
		{
			if (mScore)
				UpdateSprite(mScore.Score);
		}

		public void UpdateSprite(int index)
		{
			if (targetImage)
				for (var i = 0; i < spriteList.Length; i++)
					targetImage.sprite = spriteList[index];

			if (targetSpriteRenderer)
				for (var i = 0; i < spriteList.Length; i++)
					targetSpriteRenderer.sprite = spriteList[index];
		}
	}
}