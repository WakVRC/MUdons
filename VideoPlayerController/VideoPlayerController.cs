
using UdonSharp;
using UdonSharp.Video;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VideoPlayerController : MBase
	{
		[SerializeField] private USharpVideoPlayer usharpVideoPlayer;
		[SerializeField] private VideoControlHandler videoControlHandler;
		[SerializeField] private Transform videoDatasParent;
		[SerializeField] private VideoPlayerControllerUI[] UIs;

		public VideoData[] VideoDatas
		{
			get
			{
				if (_videoDatas == null)
					_videoDatas = videoDatasParent.GetComponentsInChildren<VideoData>();

				return _videoDatas;
			}
		}
		private VideoData[] _videoDatas;

		private void Start()
		{
			InitUI();
		}

		private void InitUI()
		{
			foreach (var ui in UIs)
				ui.Init(this);
		}

		public void PlayVideo(int index) => usharpVideoPlayer.PlayVideo(VideoDatas[index].VRCUrl);
		public void StopVideo()
		{
			MDebugLog(nameof(StopVideo));
			usharpVideoPlayer.StopVideo();
		}
		public void PauseVideo()
		{
			MDebugLog(nameof(PauseVideo));
			SetOwner(usharpVideoPlayer.gameObject);
			videoControlHandler.OnPlayButtonPress();
		}
	}
}