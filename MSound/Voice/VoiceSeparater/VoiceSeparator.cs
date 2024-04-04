using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceSeparator : VoiceUpdater
	{
		[SerializeField] private VoiceManager voiceManager;
		[SerializeField] private AreaTagger[] areaTaggers;

		public override void UpdateVoice()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null)
				return;

			string localTags = string.Empty;

			foreach (var areaTagger in areaTaggers)
			{
				string tag =
					Networking.LocalPlayer.GetPlayerTag($"{Networking.LocalPlayer.playerId}{areaTagger.Tag}");

				if (tag == null)
					tag = FALSE_STRING;

				localTags += tag;
			}

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				VRCPlayerApi player = voiceManager.PlayerApis[i];

				if (player == Networking.LocalPlayer)
					continue;

				string targetTags = string.Empty;
				foreach (var areaTagger in areaTaggers)
				{
					string tag = Networking.LocalPlayer.GetPlayerTag($"{player.playerId}{areaTagger.Tag}");

					if (tag == null)
						tag = FALSE_STRING;

					targetTags += tag;
				}

				bool equal = localTags == targetTags;

				MDebugLog($"{Networking.LocalPlayer.playerId + localTags}, {player.playerId + targetTags}, == {equal}");

				// 같은 방 or 밖 ? Default : Mute
				// 대기 공간 or 금고 ? Default : Mute

				voiceManager.VoiceStates[i] = ((voiceManager.VoiceStates[i] != VoiceState.Mute) && equal)
					? VoiceState.Default
					: VoiceState.Mute;
			}
		}
	}
}