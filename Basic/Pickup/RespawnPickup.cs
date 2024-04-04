using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RespawnPickup : MBase
    {
        [SerializeField] private VRCObjectSync[] objectSyncs;

        public void RespawnAll()
        {
            foreach (var objectSync in objectSyncs)
            {
                SetOwner(objectSync.gameObject);
                objectSync.Respawn();
            }
        }
    }
}