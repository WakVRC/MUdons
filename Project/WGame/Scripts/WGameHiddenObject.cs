
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
	public class WGameHiddenObject : MBase
	{
		[SerializeField] private Sprite[] playerIcons;
		[SerializeField] private Sprite nullIcon;

		[SerializeField] private GameObject button;
		[SerializeField] private GameObject button2;
		[SerializeField] private GameObject effect;
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI nameText;

		[SerializeField] private WGameHiddenManager gameHiddenManager;

		public const int NO_ONE = -2;

		[UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(OwnerIndex))] private int _ownerIndex = NO_ONE;
		public int OwnerIndex
		{
			get => _ownerIndex;
			set
			{
				_ownerIndex = value;
				OnOwnerChange();
			}
		}

		private void OnOwnerChange()
		{
			button.SetActive(_ownerIndex == NO_ONE);
			button2.SetActive(_ownerIndex != NO_ONE);
			effect.SetActive(_ownerIndex != NO_ONE);

			if (_ownerIndex != NO_ONE)
			{
				Sprite sprite = _ownerIndex != NONE_INT ? playerIcons[_ownerIndex] : nullIcon;
				string _name = _ownerIndex != NONE_INT ? $"{waktaNicknameDic[_ownerIndex]} 발견 !" : "이파리가 발견 !";

				// _name += "\n<size=15>이세돌 데뷔 2주년을 축하합니다 !";

				image.sprite = sprite;
				nameText.text = _name;
			}

			gameHiddenManager.TryOpenDoor();
		}

		private void Start()
		{
			OnOwnerChange();
		}

		public void Find()
		{
			SetOwner();
			OwnerIndex = GetWaktaIndexByDisplayName(Networking.LocalPlayer.displayName);
			RequestSerialization();
		}
	}
}