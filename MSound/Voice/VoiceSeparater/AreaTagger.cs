using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class AreaTagger : MBase
	{
		[Header("_" + nameof(AreaTagger))]
		[SerializeField] private VoiceManager voiceManager;
		[field:SerializeField] public VoiceAreaTag Tag {get; private set;}

		private Bounds[] boundsArray;
		[SerializeField] private BoxCollider[] areaColliders;

		[SerializeField] private CustomBool someoneIn;
		[SerializeField] private CustomBool localPlayerIn;

		private void Start()
		{
			boundsArray = new Bounds[areaColliders.Length];
			for (int i = 0; i < boundsArray.Length; i++)
			{
				// boundsArray[i] = new Bounds(areaColliders[i].center, areaColliders[i].size);
				boundsArray[i] = areaColliders[i].bounds;
			}
		}

		private void Update()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null ||
				voiceManager.PlayerApis.Length != VRCPlayerApi.GetPlayerCount())
			{
				if (someoneIn)
					someoneIn.SetValue(false);
				if (localPlayerIn)
					localPlayerIn.SetValue(false);
				return;
			}

			bool _someoneIn = false;
			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				Vector3 playerPos = voiceManager.PlayerApis[i].GetPosition();
				bool isin = IsIn(voiceManager.PlayerApis[i].playerId, playerPos);
				_someoneIn = _someoneIn || isin;
			}
			if (someoneIn)
				someoneIn.SetValue(_someoneIn);

			bool _localPlayerIn = IsIn(Networking.LocalPlayer.playerId, Networking.LocalPlayer.GetPosition());
			if (localPlayerIn)
				localPlayerIn.SetValue(_localPlayerIn);
		}

		public bool IsIn(int playerID, Vector3 playerPos)
		{
			bool isin = false;

			foreach (var bounds in boundsArray)
			{
				if (bounds.Contains(playerPos))
				{
					isin = true;
					break;
				}
			}

			MDebugLog($"{playerID}{Tag}" + (isin ? TRUE_STRING : FALSE_STRING));
			Networking.LocalPlayer.SetPlayerTag($"{playerID}{Tag}", isin ? TRUE_STRING : FALSE_STRING);

			return isin;
		}
	}
}