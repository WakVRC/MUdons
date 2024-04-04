
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceAmplification_Area : VoiceAmplification
	{
		[Header("_" + nameof(VoiceAmplification_Area))]
		[SerializeField] private AreaTagger areaTagger;

		protected override bool IsTarget(VRCPlayerApi playerAPI)
		{
			bool isTarget = areaTagger.IsIn(playerAPI.playerId, playerAPI.GetPosition()); ;
			MDebugLog(nameof(IsTarget) + isTarget);
			return isTarget;
		}
	}
}