using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceRooms : VoiceUpdater
	{
		[SerializeField] private VoiceManager voiceManager;
		[SerializeField] private VoiceRoom[] voiceRooms;

		public override void UpdateVoice()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null)
				return;

			string localTags = string.Empty;

			foreach (var room in voiceRooms)
			{
				string tag =
					Networking.LocalPlayer.GetPlayerTag($"{Networking.LocalPlayer.playerId}{room.AreaTag}");

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
				foreach (var room in voiceRooms)
				{
					string tag = Networking.LocalPlayer.GetPlayerTag($"{player.playerId}{room.AreaTag}");

					if (tag == null)
						tag = FALSE_STRING;

					targetTags += tag;
				}

				bool equal = localTags == targetTags;

				MDebugLog($"{Networking.LocalPlayer.playerId + localTags}, {player.playerId + targetTags}, == {equal}");

				voiceManager.VoiceStates[i] = ((voiceManager.VoiceStates[i] != VoiceState.Mute) && equal)
					? VoiceState.Default
					: VoiceState.Mute;
			}
		}
	}
}