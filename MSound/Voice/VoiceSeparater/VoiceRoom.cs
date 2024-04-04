using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class VoiceRoom : MBase
	{
		[Header("_" + nameof(VoiceRoom))]
		[SerializeField] private VoiceManager voiceManager;
		[SerializeField] private VoiceAreaTag areaTag = VoiceAreaTag.ROOM_1;
		public VoiceAreaTag AreaTag => areaTag;

		[SerializeField] private MTarget[] mTargets;
		[SerializeField] private SyncedBool[] syncedBools;
		public SyncedBool[] SyncedBools => syncedBools;
		[SerializeField] private float updateTerm = .5f;

		// [SerializeField] private SyncedBool isLocked;
		[SerializeField] private TimeEvent isLocked_TimeEvent;

		private void Start() => UpdateVoiceLoop();
		public void UpdateVoiceLoop()
		{
			SendCustomEventDelayedSeconds(nameof(UpdateVoiceLoop), updateTerm);
			UpdateVoice();
		}

		public void UpdateVoice()
		{
			if (NotOnline)
				return;

			if (voiceManager.PlayerApis == null ||
				voiceManager.PlayerApis.Length != VRCPlayerApi.GetPlayerCount())
				return;

			bool localPlayerIsInRoom = false;
			for (int i = 0; i < mTargets.Length; i++)
			{
				if (mTargets[i].IsLocalPlayerTarget && syncedBools[i].Value)
				{
					localPlayerIsInRoom = true;
					break;
				}
			}
			Networking.LocalPlayer.SetPlayerTag($"{Networking.LocalPlayer.playerId}{areaTag}", localPlayerIsInRoom ? TRUE_STRING : FALSE_STRING);

			for (int i = 0; i < voiceManager.PlayerApis.Length; i++)
			{
				int targetPlayerID = voiceManager.PlayerApis[i].playerId;
				bool targetPlayerIsInRoom = false;

				for (int j = 0; j < mTargets.Length; j++)
				{
					if (mTargets[j].CurTargetPlayerID == targetPlayerID && syncedBools[j].Value)
					{
						targetPlayerIsInRoom = true;
						break;
					}
				}
				Networking.LocalPlayer.SetPlayerTag($"{targetPlayerID}{areaTag}", targetPlayerIsInRoom ? TRUE_STRING : FALSE_STRING);
			}
		}

		public void GoRoom()
		{
			// MDebugLog(nameof(GoRoom));

			int localPlayerNum = GetLocalPlayerNum();
			if (localPlayerNum == NONE_INT)
				return;

			int inPlayerCount = 0;
			for (int i = 0; i < mTargets.Length; i++)
			{
				if (syncedBools[i].Value)
					inPlayerCount++;
			}

			if (syncedBools[localPlayerNum].Value)
			{
				syncedBools[localPlayerNum].SetValue(false);

				// if ((inPlayerCount == 1) && (isLocked.Value == true))
				if ((inPlayerCount == 1) && (isLocked_TimeEvent.IsExpired == false))
				{
					// isLocked.SetValue(false);
					isLocked_TimeEvent.ResetTime();
				}
			}
			else
			{
				// if (isLocked.Value)
				if (isLocked_TimeEvent.IsExpired == false)
					return;

				syncedBools[localPlayerNum].SetValue(true);
			}
		}

		private int GetLocalPlayerNum()
		{
			for (int i = 0; i < mTargets.Length; i++)
			{
				if (mTargets[i].IsLocalPlayerTarget)
					return i;
			}

			return NONE_INT;
		}

		public void Lock()
		{
			// isLocked.SetValue(true);
			isLocked_TimeEvent.SetTime();
		}
		public void Unlock()
		{
			// isLocked.SetValue(false);
			isLocked_TimeEvent.ResetTime();
		}

		public void ResetSync()
		{
			for (int i = 0; i < mTargets.Length; i++)
				syncedBools[i].SetValue(false);

			Unlock();
		}
	}
}