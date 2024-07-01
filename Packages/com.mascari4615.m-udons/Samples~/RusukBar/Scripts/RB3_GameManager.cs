﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class RB3_GameManager : MBase
	{
		/*[SerializeField] private GameObject mainUI;
		[SerializeField] private Transform[] vrUIPos;
		public CanvasManager[] canvasManagers;
		private bool isVR = false;

		[Header("Objects")][SerializeField] private GameObject postprocessObject;

		[SerializeField] private GameObject piano;
		[SerializeField] private GameObject guitar;
		[SerializeField] private GameObject drum;
		[SerializeField] private GameObject mike;
		[SerializeField] private Collider[] colliders;

		[Header("Objects")][SerializeField] private VRC_Pickup[] cocktails;

		[SerializeField] private VRC_Pickup[] pizzas;
		[SerializeField] private VRC_Pickup[] chesses;
		[SerializeField] private AudioClip[] sfxDB;

		[SerializeField] private ObjectActive mirror;
		[SerializeField] private ObjectActive mirrorQuility;
		private int curVRUIPosIndex = -1;

		[Header("ETC")] private bool isBellOn = true;

		private QvPen_Pen[] pens;

		[Header("Sounds")] private AudioSource sfxAS;

		private void Start()
		{
			Debug.Log(nameof(Start));

			pens = GameObject.Find("Pens").GetComponentsInChildren<QvPen_Pen>();
			sfxAS = transform.Find("Sound").Find("SFX").GetComponent<AudioSource>();

			mainUI.SetActive(false);
			// mainUI.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
			mike.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

			isVR = Networking.LocalPlayer.IsUserInVR();

			if (isVR)
				ToggleColliders();

			ToggleVRUI0();
		}

		private void Update()
		{
			if (isVR)
				return;

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				foreach (var item in canvasManagers)
					item.ChangeCocktail();
				mainUI.SetActive(true);
				canvasManagers[0].Init();
			}
			else if (Input.GetKeyUp(KeyCode.Tab))
			{
				foreach (var item in canvasManagers)
					item.ChangeCocktail();
				mainUI.SetActive(false);
				canvasManagers[0].Init();
			}
		}

		public void ToggleMirror()
		{
			mirror.ToggleActive();
		}

		public void ToggleMirrorQuility()
		{
			mirrorQuility.ToggleActive();
		}

		public override void OnPlayerJoined(VRCPlayerApi player)
		{
			if (player == Networking.LocalPlayer) SendCustomNetworkEvent(NetworkEventTarget.All, nameof(Bell));
			Debug.Log(nameof(OnPlayerJoined));
		}

		public void Bell()
		{
			if (isBellOn)
				sfxAS.PlayOneShot(sfxDB[0]);
		}

		public void ToggleBell()
		{
			isBellOn = !isBellOn;
			foreach (var canvas in canvasManagers)
				canvas.SetBellToggleImage(isBellOn);
		}

		public void MusicPause(bool isPaused)
		{
			foreach (var canvas in canvasManagers)
				canvas.UpdateMusicPlayIcon(isPaused);
		}

		public void ToggleMike()
		{
			ToggleObject(mike);
			foreach (var canvas in canvasManagers)
				canvas.SetMikeActiveToggleImage(mike.activeSelf);
		}

		public void TogglePostProcess()
		{
			ToggleObject(postprocessObject);
			foreach (var canvas in canvasManagers)
				canvas.SetPostProcessToggleImage(postprocessObject.activeSelf);
		}

		public void TogglePiano()
		{
			ToggleObject(piano);
			foreach (var canvas in canvasManagers)
				canvas.SetPianoToggleImage(piano.activeSelf);
		}

		public void ToggleDrum()
		{
			ToggleObject(drum);
			foreach (var canvas in canvasManagers)
				canvas.SetDrumToggleImage(drum.activeSelf);
		}

		public void ToggleGuitar()
		{
			ToggleObject(guitar);
			foreach (var canvas in canvasManagers)
				canvas.SetGuitarToggleImage(guitar.activeSelf);
		}

		public void ToggleObject(GameObject target)
		{
			target.SetActive(!target.activeSelf);
		}

		public void ToggleColliders()
		{
			foreach (var collider in colliders)
			{
				if (collider == null)
					continue;
				collider.enabled = !collider.enabled;
			}

			foreach (var canvas in canvasManagers)
				canvas.SetColliderToggleImage(colliders[0].enabled);
		}

		public void ButtonSFX()
		{
			sfxAS.PlayOneShot(sfxDB[1]);
		}

		public void ToggleVRUI(int posIndex)
		{
			if (curVRUIPosIndex == posIndex)
			{
				canvasManagers[1].gameObject.SetActive(!canvasManagers[1].gameObject.activeSelf);
			}
			else
			{
				canvasManagers[1].transform.SetPositionAndRotation(vrUIPos[curVRUIPosIndex = posIndex].position,
					vrUIPos[curVRUIPosIndex = posIndex].rotation);
				canvasManagers[1].gameObject.SetActive(false);
				canvasManagers[1].gameObject.SetActive(true);
			}
		}

		public void ToggleVRUI0()
		{
			ToggleVRUI(0);
		}

		public void ToggleVRUI1()
		{
			ToggleVRUI(1);
		}

		public void ToggleVRUI2()
		{
			ToggleVRUI(2);
		}

		public void ResetAllPos()
		{
			ResetCocktailPos();
			ResetPizzaPos();
			ResetChessPos();
		}

		public void ResetCocktailPos()
		{
			ResetPos(cocktails);
		}

		public void ResetPizzaPos()
		{
			ResetPos(pizzas);
		}

		public void ResetChessPos()
		{
			ResetPos(chesses);
		}

		private void ResetPos(VRC_Pickup[] pickups)
		{
			foreach (var pickup in pickups)
			{
				if (!Networking.IsOwner(Networking.LocalPlayer, pickup.gameObject))
					Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
				pickup.Drop();
				pickup.transform.position = Vector3.down * 444f;
			}
		}

		public void ResetPens_Global()
		{
			if (Networking.LocalPlayer.isMaster) SendCustomNetworkEvent(NetworkEventTarget.All, nameof(ResetPens));
		}

		public void ResetPens()
		{
			foreach (var pen in pens) pen._Respawn();
		}

		public void ClearPens_Global()
		{
			if (Networking.LocalPlayer.isMaster) SendCustomNetworkEvent(NetworkEventTarget.All, nameof(ClearPens));
		}

		public void ClearPens()
		{
			foreach (var pen in pens) pen._Clear();
		}*/
	}
}