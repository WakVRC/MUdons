using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class MTarget_Example : UdonSharpBehaviour
	{
		[SerializeField] private MTarget mTarget;
		[SerializeField] private Transform teleportPos;

		public override void Interact()
		{
			SendCustomNetworkEvent(NetworkEventTarget.All, nameof(TryTeleport));
		}

		public void TryTeleport()
		{
			if (mTarget.IsLocalPlayerTarget)
				Networking.LocalPlayer.TeleportTo(teleportPos.position, teleportPos.rotation);
		}
	}
}