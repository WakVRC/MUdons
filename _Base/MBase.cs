using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
	public class MBase : UdonSharpBehaviour
	{
		protected const string DEBUG_PREFIX = "DEBUG";

		protected const char DATA_PACK_SEPARATOR = '_';
		protected const char DATA_SEPARATOR = '@';

		protected const int NONE_INT = -1;
		protected const string NONE_STRING = "SANS";
		protected const string TRUE_STRING = "TRUE";
		protected const string FALSE_STRING = "FALSE";

		[Header("_" + nameof(MBase))]
		[SerializeField] protected bool DEBUG;

		#region HACKs
		protected void MDebugLog(string log = NONE_STRING)
		{
			// To Not Log Error When Player Left The World.
			if (NotOnline)
				return;

			if (DEBUG)
				Debug.Log($"{DEBUG_PREFIX}, {Networking.LocalPlayer.playerId}, {gameObject.name} : {log}");
		}

		protected bool NotOnline => (Networking.LocalPlayer == null);

		protected void SetOwner(GameObject targetObject = null)
		{
			if (targetObject == null)
				targetObject = gameObject;

			if (!IsOwner(targetObject))
				Networking.SetOwner(Networking.LocalPlayer, targetObject);
		}

		protected bool IsOwner(GameObject targetObject = null)
		{
			if (NotOnline)
				return false;

			if (targetObject == null)
				targetObject = gameObject;

			return Networking.LocalPlayer.IsOwner(targetObject);
		}

		protected VRCPlayerApi[] Players => VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);

		protected VRCPlayerApi GetPlayerByName(string targetName)
		{
			VRCPlayerApi[] players = Players;

			foreach (var player in players)
				if (player.displayName == targetName)
					return player;

			return null;
		}

		protected string LocalPlayerName => Networking.LocalPlayer.displayName;
		protected bool IsLocalPlayerVR => Networking.LocalPlayer.IsUserInVR();

		protected bool IsLocalPlayer(VRCPlayerApi player) => !NotOnline && (Networking.LocalPlayer == player);
		protected bool IsLocalPlayerID(int id) => !NotOnline && (Networking.LocalPlayer.playerId == id);
		protected bool IsLocalPlayerName(string targetName) => !NotOnline && (Networking.LocalPlayer.displayName == targetName);

		protected bool IsDigit(string s)
		{
			if (s.Length <= 0)
				return false;

			s = s.TrimEnd('\n', ' ', (char)8203);
			if (s.Length == 0)
				return false;

			foreach (var c in s)
				if (!char.IsDigit(c))
					return false;

			return true;
		}

		protected bool IsLocalPlayerHolding(VRC_Pickup pickup)
		{
			if (NotOnline)
				return false;

			return Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) == pickup ||
				   Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) == pickup;
		}

		protected void TP(Transform tr) => Networking.LocalPlayer.TeleportTo(tr.position, tr.rotation);
		#endregion

		#region Color
		protected readonly Color WHITE = Color.white;
		protected readonly Color WHITE_GRAY = new Color(184f / 255f, 181f / 255f, 185f / 255f);
		protected readonly Color GRAY = new Color(69f / 255f, 68f / 255f, 79f / 255f);
		protected readonly Color BLACK = new Color(38f / 255f, 43f / 255f, 68f / 255f);

		protected readonly Color RED = new Color(177f / 255f, 82f / 255f, 84f / 255f);
		protected readonly Color GREEN = new Color(195f / 255f, 210f / 255f, 113f / 255f);
		protected readonly Color BLUE = new Color(76f / 255f, 130f / 255f, 199f / 255f);

		protected readonly Color COLOR_WAKGOOD = new Color(24f / 255f, 69f / 255f, 51f / 255f); // 24 69 51
		
		protected readonly Color COLOR_INE = new Color(137f / 255f, 55f / 255f, 221f / 255f); // 137 55 221
		protected readonly Color COLOR_JINGBURGER = new Color(239f / 255f, 168f / 255f, 95f / 255f); // 239 168 95
		protected readonly Color COLOR_LILPA = new Color(67f / 255f, 58f / 255f, 99f / 255f); // 67 58 99
		protected readonly Color COLOR_JURURU = new Color(253f / 255f, 8f / 255f, 138f / 255f); // 253 8 138
		protected readonly Color COLOR_GOSEGU = new Color(71f / 255f, 128f / 255f, 195f / 255f); // 71 128 195
		protected readonly Color COLOR_VIICHAN = new Color(150f / 255f, 191f / 255f, 45f / 255f); // 150 191 45

		protected Color GetGreenOrRed(bool boolVar) => boolVar ? GREEN : RED;
		protected Color GetWhiteOrGray(bool boolVar) => boolVar ? WHITE : GRAY;
		protected Color GetBlackOrGray(bool boolVar) => boolVar ? BLACK : GRAY;
		protected Color GetWhiteOrBlack(bool boolVar) => boolVar ? WHITE : BLACK;

		protected Color GetISDColorByDisplayName(string displayName)
		{
			Color color = Color.white;

			switch (displayName)
			{
				case "gosegu":
					color = COLOR_GOSEGU;
					break;
				case "LILPAAA":
					color = COLOR_LILPA;
					break;
				case "VIichan":
					color = COLOR_VIICHAN;
					break;
				case "INE_아이네":
					color = COLOR_INE;
					break;
				case "jururu_twitch":
					color = COLOR_JURURU;
					break;
				case "jingburger":
					color = COLOR_JINGBURGER;
					break;
				case "VRwakgood":
					color = COLOR_WAKGOOD;
					break;
				default:
					break;
			}

			return color;
		}
		#endregion

		#region Wakgood
		protected string WakgoodName => "VRwakgood";
		protected bool IsLocalWakgood => Networking.LocalPlayer.displayName.Equals(WakgoodName);
		protected VRCPlayerApi Wakgood => GetPlayerByName(WakgoodName);

		// TODO: 어칼님께서 업데이트 해주신 걸로 수정
		protected readonly string[] waktaDisplayNameDic =
		{
			"VRwakgood",

			"INE_아이네",
			"jingburger",
			"LILPAAA",
			"jururu_twitch",
			"gosegu",
			"VIichan",

			"병장 곽춘식",
			"KwonMin",
			"김치만두번영택사스가",
			"Nosferatu Hodd",
			"망령벌레",
			"Dr Dopamine",
			"독고혜지",
			"roentgenium",
			"부정형인간",
			"비밀소녀",
			"비즈니스킴 6ecf",
			"새우튀김70",
			"-소피아-",
			"왁파고_",
			"이덕수할아바이",
			"카르나르 융터르 39c8",
			"Cally Carly Davidson c76f",
			"(풍신)",
			"Freeter",
			"HAKU0089",
			"RUSUK",
			"Hiki King",
		};

		protected readonly string[] waktaNicknameDic =
		{
			"우왁굳",

			"아이네",
			"징버거",
			"릴파",
			"주르르",
			"고세구",
			"비챤",

			"곽춘식",
			"권민",
			"김치만두번영택사스가",
			"노스페라투 호드",
			"단답벌레",
			"도파민",
			"독고혜지",
			"뢴트게늄",
			"부정형인간",
			"비밀소녀",
			"비즈니스킴",
			"새우튀김",
			"소피아",
			"왁파고",
			"이덕수할아바이",
			"카르나르융터르",
			"캘리칼리 데이비슨",
			"풍신",
			"프리터",
			"하쿠0089",
			"해루석",
			"히키킹",
		};

		protected int GetWaktaIndexByDisplayName(string displayName)
		{
			for (int i = 0; i < waktaDisplayNameDic.Length; i++)
			{
				if (waktaDisplayNameDic[i] == displayName)
					return i;
			}

			return NONE_INT;
		}
		#endregion
	}
}