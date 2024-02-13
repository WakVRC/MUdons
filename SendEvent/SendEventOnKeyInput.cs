using System;
using UdonSharp;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Mascari4615
{
	// [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class SendEventOnKeyInput : MEventSender
	{
		[Header("_" + nameof(SendEventOnKeyInput))]
		[SerializeField] private KeyCode keyCode;

		[SerializeField] private bool whenGetKeyDown;
		[SerializeField] private bool whenGetKeyUp;

		private void Update()
		{
			if (Input.GetKeyDown(keyCode))
				SendEvents();

			if (Input.GetKeyUp(keyCode))
				SendEvents();
		}
	}
}