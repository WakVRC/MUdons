
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VideoPlayerControllerButton : MBase
	{
		[SerializeField] private TextMeshProUGUI videoNameText;
		private VideoPlayerControllerUI videoPlayerControllerUI;
		private int index;

		public void Init(VideoPlayerControllerUI videoPlayerControllerUI, int index, string videoName)
		{
			this.videoPlayerControllerUI = videoPlayerControllerUI;
			this.index = index;
			videoNameText.text = videoName;
		}

		public void Click()
		{
			videoPlayerControllerUI.PlayVideo(index);
		}
	}
}