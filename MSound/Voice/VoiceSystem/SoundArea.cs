using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SoundArea : UdonSharpBehaviour
    {
        [SerializeField] private string myTag;
        [SerializeField] private VoiceByRole voiceManager;

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            Debug.Log(player.displayName +
                      $" {player.playerId} : {Networking.LocalPlayer.GetPlayerTag(player.displayName)} => " + myTag);

            Networking.LocalPlayer.SetPlayerTag(player.displayName, myTag);

            voiceManager.UpdateSound();
        }
    }
}