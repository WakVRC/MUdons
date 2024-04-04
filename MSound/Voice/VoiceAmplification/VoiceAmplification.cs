using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public abstract class VoiceAmplification : VoiceUpdater
	{
		[Header("_" + nameof(VoiceAmplification))]
		[SerializeField] protected VoiceManager voiceManager;
		[SerializeField] private CustomBool state;

		public override void UpdateVoice()
		{
			if (voiceManager.PlayerApis == null)
				return;

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				bool isTargetPlayer = IsTarget(voiceManager.PlayerApis[i]);
				bool amplification = isTargetPlayer && ((state == null) || state.Value);

				voiceManager.VoiceStates[i] = amplification ? VoiceState.Amplification :
					usePrevData ? voiceManager.VoiceStates[i] : VoiceState.Default;
			}
		}

		protected abstract bool IsTarget(VRCPlayerApi playerApi);

		public void SetState(bool on)
		{
			if (state)
				state.SetValue(on);
		}
	}
}