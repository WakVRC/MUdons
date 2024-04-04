
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class VideoData : MBase
    {
        public VRCUrl VRCUrl => _vrcURL;
		[SerializeField] private VRCUrl _vrcURL;

        public string VideoName => videoName;
        [SerializeField] private string videoName;
    }
}