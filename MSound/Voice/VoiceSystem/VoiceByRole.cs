using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class VoiceByRole : UdonSharpBehaviour
    {
        [SerializeField] private Image idolCanHearIdolImage;
        [SerializeField] private Image idolCanHearJudgeImage;
        [SerializeField] private Image idolCanHearCrowdImage;

        [SerializeField] private Image judgeCanHearIdolImage;
        [SerializeField] private Image judgeCanHearJudgeImage;
        [SerializeField] private Image judgeCanHearCrowdImage;

        [SerializeField] private Image crowdCanHearIdolImage;
        [SerializeField] private Image crowdCanHearJudgeImage;
        [SerializeField] private Image crowdCanHearCrowdImage;

        [SerializeField] private Image cameraCanHearIdolImage;
        [SerializeField] private Image cameraCanHearJudgeImage;
        [SerializeField] private Image cameraCanHearCrowdImage;

        [SerializeField] private Transform audioScreen;
        [SerializeField] private AudioSource[] audioSources;
        [UdonSynced()] private bool cameraCanHearCrowd = true;

        [UdonSynced()] private bool cameraCanHearIdol = true;
        [UdonSynced()] private bool cameraCanHearJudge = true;
        [UdonSynced()] private bool crowdCanHearCrowd = true;

        [UdonSynced()] private bool crowdCanHearIdol = true;
        [UdonSynced()] private bool crowdCanHearJudge = true;
        [UdonSynced()] private bool idolCanHearCrowd = true;
        [UdonSynced()] private bool idolCanHearIdol = true;
        [UdonSynced()] private bool idolCanHearJudge = true;
        [UdonSynced()] private bool judgeCanHearCrowd = true;

        [UdonSynced()] private bool judgeCanHearIdol = true;
        [UdonSynced()] private bool judgeCanHearJudge = true;
        private VRCPlayerApi[] players;

        private void Start()
        {
            players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);
        }

        private void Update()
        {
            if (Networking.LocalPlayer.GetPlayerTag(Networking.LocalPlayer.displayName) == "FAIL")
            {
                audioSources[0].volume =
                    1f * ((20f - Vector3.Distance(Networking.LocalPlayer.GetPosition(), audioScreen.position)) / 20f);
                audioSources[1].volume =
                    1f * ((20f - Vector3.Distance(Networking.LocalPlayer.GetPosition(), audioScreen.position)) / 20f);
                audioSources[2].volume =
                    1f * ((20f - Vector3.Distance(Networking.LocalPlayer.GetPosition(), audioScreen.position)) / 20f);

                foreach (var player in players)
                {
                    var targetTag = Networking.LocalPlayer.GetPlayerTag(player.displayName);
                    if (targetTag == null)
                        continue;

                    if (targetTag == "STAGE")
                    {
                        player.SetVoiceDistanceFar(980 *
                                                   ((15.0f - Vector3.Distance(Networking.LocalPlayer.GetPosition(),
                                                       audioScreen.position)) / 15.0f));
                        player.SetVoiceDistanceNear(1000 *
                                                    ((15.0f - Vector3.Distance(Networking.LocalPlayer.GetPosition(),
                                                        audioScreen.position)) / 15.0f));
                        player.SetVoiceGain(24 *
                                            ((15.0f - Vector3.Distance(Networking.LocalPlayer.GetPosition(),
                                                audioScreen.position)) / 15.0f));
                    }
                    else
                    {
                        player.SetVoiceDistanceFar(25);
                        player.SetVoiceDistanceNear(0);
                        player.SetVoiceGain(15);
                    }
                }
            }
        }

        public void ToggleIdolCanHearIdol()
        {
            TryTakeOwner();
            idolCanHearIdol = !idolCanHearIdol;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleIdolCanHearJudge()
        {
            TryTakeOwner();
            idolCanHearJudge = !idolCanHearJudge;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleIdolCanHearCrowd()
        {
            TryTakeOwner();
            idolCanHearCrowd = !idolCanHearCrowd;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleJudgeCanHearIdol()
        {
            TryTakeOwner();
            judgeCanHearIdol = !judgeCanHearIdol;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleJudgeCanHearJudge()
        {
            TryTakeOwner();
            judgeCanHearJudge = !judgeCanHearJudge;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleJudgeCanHearCrowd()
        {
            TryTakeOwner();
            judgeCanHearCrowd = !judgeCanHearCrowd;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCrowdCanHearIdol()
        {
            TryTakeOwner();
            crowdCanHearIdol = !crowdCanHearIdol;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCrowdCanHearJudge()
        {
            TryTakeOwner();
            crowdCanHearJudge = !crowdCanHearJudge;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCrowdCanHearCrowd()
        {
            TryTakeOwner();
            crowdCanHearCrowd = !crowdCanHearCrowd;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCameraCanHearIdol()
        {
            TryTakeOwner();
            cameraCanHearIdol = !cameraCanHearIdol;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCameraCanHearJudge()
        {
            TryTakeOwner();
            cameraCanHearJudge = !cameraCanHearJudge;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public void ToggleCameraCanHearCrowd()
        {
            TryTakeOwner();
            cameraCanHearCrowd = !cameraCanHearCrowd;
            RequestSerialization();
            SendCustomNetworkEvent(NetworkEventTarget.All, nameof(UpdateSound));
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);

            // if (player == Networking.LocalPlayer)
            UpdateSound();
        }

        public void SetVoiceGlobal(VRCPlayerApi player, bool canHear)
        {
            player.SetVoiceDistanceFar(canHear ? 395 : 0);
            player.SetVoiceDistanceNear(canHear ? 400 : 0);
            player.SetVoiceGain(canHear ? 24 : 0);
        }

        public void TryTakeOwner()
        {
            if (!Networking.LocalPlayer.IsOwner(gameObject))
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);
        }

        public void UpdateSound()
        {
            var localTag = Networking.LocalPlayer.GetPlayerTag(Networking.LocalPlayer.displayName);

            idolCanHearIdolImage.color = idolCanHearIdol ? Color.green : Color.red;
            idolCanHearJudgeImage.color = idolCanHearJudge ? Color.green : Color.red;
            idolCanHearCrowdImage.color = idolCanHearCrowd ? Color.green : Color.red;

            judgeCanHearIdolImage.color = judgeCanHearIdol ? Color.green : Color.red;
            judgeCanHearJudgeImage.color = judgeCanHearJudge ? Color.green : Color.red;
            judgeCanHearCrowdImage.color = judgeCanHearCrowd ? Color.green : Color.red;

            crowdCanHearIdolImage.color = crowdCanHearIdol ? Color.green : Color.red;
            crowdCanHearJudgeImage.color = crowdCanHearJudge ? Color.green : Color.red;
            crowdCanHearCrowdImage.color = crowdCanHearCrowd ? Color.green : Color.red;

            cameraCanHearIdolImage.color = cameraCanHearIdol ? Color.green : Color.red;
            cameraCanHearJudgeImage.color = cameraCanHearJudge ? Color.green : Color.red;
            cameraCanHearCrowdImage.color = cameraCanHearCrowd ? Color.green : Color.red;

            foreach (var player in players)
            {
                if (player == Networking.LocalPlayer)
                    continue;

                var targetTag = Networking.LocalPlayer.GetPlayerTag(player.displayName);
                if (targetTag == null)
                    continue;

                if (targetTag == "STAGE" && localTag == "STAGE")
                {
                    /*player.SetVoiceDistanceFar(395);
                    player.SetVoiceDistanceNear(400);
                    player.SetVoiceGain(24);*/

                    if (IsStaff(Networking.LocalPlayer))
                    {
                        if (IsIdol(player))
                            SetVoiceGlobal(player, cameraCanHearIdol);
                        else if (IsJudge(player))
                            SetVoiceGlobal(player, cameraCanHearJudge);
                        else if (!IsStaff(player))
                            SetVoiceGlobal(player, cameraCanHearCrowd);
                    }
                    else if (IsJudge(Networking.LocalPlayer))
                    {
                        if (IsIdol(player))
                            SetVoiceGlobal(player, judgeCanHearIdol);
                        else if (IsJudge(player))
                            SetVoiceGlobal(player, judgeCanHearJudge);
                        else if (!IsStaff(player))
                            SetVoiceGlobal(player, judgeCanHearCrowd);
                    }
                    else if (IsIdol(Networking.LocalPlayer))
                    {
                        if (IsIdol(player))
                            SetVoiceGlobal(player, idolCanHearIdol);
                        else if (IsJudge(player))
                            SetVoiceGlobal(player, idolCanHearJudge);
                        else if (!IsStaff(player))
                            SetVoiceGlobal(player, idolCanHearJudge);
                    }
                    else
                    {
                        if (IsIdol(player))
                            SetVoiceGlobal(player, crowdCanHearIdol);
                        else if (IsJudge(player))
                            SetVoiceGlobal(player, crowdCanHearJudge);
                        else if (!IsStaff(player))
                            SetVoiceGlobal(player, cameraCanHearCrowd);
                    }
                }
                else
                {
                    player.SetVoiceDistanceFar(25);
                    player.SetVoiceDistanceNear(0);
                    player.SetVoiceGain(15);
                }
            }
        }

        public void UpdateLocalTag()
        {
            var localTag = Networking.LocalPlayer.GetPlayerTag(Networking.LocalPlayer.displayName);

            if (localTag == "STAGE")
            {
                audioSources[0].volume = .8f;
                audioSources[1].volume = .6f;
                audioSources[2].volume = .6f;
            }
            else if (localTag == "UNDER")
            {
                audioSources[0].volume = 0f;
                audioSources[1].volume = 0f;
                audioSources[2].volume = 0f;
            }
            else if (localTag == "FAIL")
            {
                audioSources[0].volume = 0;
                audioSources[1].volume = .8f;
                audioSources[2].volume = .8f;
            }
        }

        #region Role

        public bool IsStaff(VRCPlayerApi player)
        {
            return player.displayName.Contains("-CAM") ||
                   player.displayName == "클로버_" ||
                   player.displayName == "SYUN__" ||
                   player.displayName == "_힉민_" ||
                   player.displayName == "카모뜨린";
        }

        public bool IsJudge(VRCPlayerApi player)
        {
            return player.displayName == "- 붐 -" ||
                   player.displayName == "-아이키-" ||
                   player.displayName == "-펭수-" ||
                   player.displayName == "-바다-"; /*||
			player.displayName == "4B_NARU"*/
        }

        public bool IsIdol(VRCPlayerApi player)
        {
            return player.displayName == "-서리태-" ||
                   player.displayName == "-도파민-" ||
                   player.displayName == "-바림-" ||
                   player.displayName == "-도화-" ||
                   player.displayName == "-무너-" ||
                   player.displayName == "-로즈-" ||
                   player.displayName == "-크앙-" ||
                   player.displayName == "-니케나-" ||
                   player.displayName == "-시계는와치-" ||
                   player.displayName == "-집순희-" ||
                   player.displayName == "-리엔-" ||
                   player.displayName == "-김세레나-" ||
                   player.displayName == "-니모-" ||
                   player.displayName == "-치어-" ||
                   player.displayName == "-차차다섯공주-" ||
                   player.displayName == "-키 키-" ||
                   player.displayName == "-화의자-" ||
                   player.displayName == "-차도도-" ||
                   player.displayName == "-정호랑-" ||
                   player.displayName == "-체리-" ||
                   player.displayName == "-뚱냥이-" ||
                   player.displayName == "-하이루-" ||
                   player.displayName == "-캐서린-" ||
                   player.displayName == "-순대내장-" ||
                   player.displayName == "-짜루-" ||
                   player.displayName == "-유주얼-" ||
                   player.displayName == "-세라-" ||
                   player.displayName == "-루비-" ||
                   player.displayName == "-예니콜-" ||
                   player.displayName == "-라스칼-"; /*||
			player.displayName == "섄디 한"*/
        }

        #endregion
    }
}