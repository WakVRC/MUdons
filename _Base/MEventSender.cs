using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Mascari4615
{
	public class MEventSender : MBase
	{
		[Header("_" + nameof(MEventSender))]
		[SerializeField] protected UdonBehaviour[] targetUdons;
		[SerializeField] protected string[] eventNames;
		[SerializeField] protected bool sendGlobal;

		protected void SendEvents()
		{
			MDebugLog($"{nameof(SendEvents)}");

			if (targetUdons == null || targetUdons.Length == 0)
				return;

			if (eventNames == null || eventNames.Length == 0)
			{
				MDebugLog($"{nameof(SendEvents)} : No Events");
				return;
			}

			for (int i = 0; i < targetUdons.Length; i++)
			{
				if (sendGlobal)
					targetUdons[i].SendCustomNetworkEvent(NetworkEventTarget.All, eventNames[i]);
				else
					targetUdons[i].SendCustomEvent(eventNames[i]);
			}
		}
	}
}