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

			// 타겟이 아니라면 무시
			foreach (MTarget mTarget in mTargets)
			{
				if (mTarget.IsLocalPlayerTarget)
					return;
			}

			// 타겟이라면 보이스 상태를 초기화
			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
				voiceManager.VoiceStates[i] = VoiceState.Default;
		}
	}
}