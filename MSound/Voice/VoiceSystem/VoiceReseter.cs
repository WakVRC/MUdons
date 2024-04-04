using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceReseter : VoiceUpdater
	{
		[SerializeField] private VoiceManager voiceManager;
		[SerializeField] private MTarget[] mTargets;

		public override void UpdateVoice()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null)
				return;

			foreach (MTarget mTarget in mTargets)
			{
				if (mTarget.IsLocalPlayerTarget)
					return;
			}

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
				voiceManager.VoiceStates[i] = VoiceState.Default;
		}
	}
}