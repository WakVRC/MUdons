
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VideoPlayerControllerUI : MBase
	{
		[SerializeField] private Transform videoButtonsParent;
		private VideoPlayerController videoPlayerController;

		public void Init(VideoPlayerController videoPlayerController)
		{
			this.videoPlayerController = videoPlayerController;

			VideoPlayerControllerButton[] videoPlayerControllerButtons = videoButtonsParent.GetComponentsInChildren<VideoPlayerControllerButton>();
			for (int i = 0; i < videoPlayerControllerButtons.Length; i++)
			{
				if (i < videoPlayerController.VideoDatas.Length)
					videoPlayerControllerButtons[i].Init(this, i, videoPlayerController.VideoDatas[i].VideoName);
				else
					videoPlayerControllerButtons[i].gameObject.SetActive(false);
			}
		}

		public void PlayVideo(int index) => videoPlayerController.PlayVideo(index);
		public void StopVideo()
		{
			MDebugLog(nameof(StopVideo));
			videoPlayerController.StopVideo();
		}
		public void PauseVideo()
		{
			MDebugLog(nameof(PauseVideo));
			videoPlayerController.PauseVideo();
		}
	}
}