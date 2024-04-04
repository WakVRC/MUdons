using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceAmplificationTargetNear : VoiceUpdater
	{
		[SerializeField] private VoiceManager voiceManager;
		public MTarget MTarget => targetPlayer;
		[SerializeField] private MTarget targetPlayer;
		[SerializeField] private SyncedBool state;

		[SerializeField] private float amplificationDistance = 10;

		public override void UpdateVoice()
		{
			if (voiceManager.PlayerApis == null)
				return;

			if ((targetPlayer == null) || (targetPlayer.CurTargetPlayerID == NONE_INT))
				return;

			VRCPlayerApi targetPlayerAPI = targetPlayer.GetTargetPlayerAPI();

			if (targetPlayerAPI == null)
				return;

			Vector3 targetPos = targetPlayerAPI.GetPosition();

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				Vector3 thisPos = VRCPlayerApi.GetPlayerById(voiceManager.PlayerApis[i].playerId).GetPosition();
				bool amplification = (state.SyncValue && (Vector3.Distance(thisPos, targetPos) < amplificationDistance));
				voiceManager.VoiceStates[i] = amplification ? VoiceState.Amplification :
					usePrevData ? voiceManager.VoiceStates[i] : VoiceState.Default;
			}
		}

		public void SetState(bool on)
		{
			state.SetValue(on);
		}
	}
}