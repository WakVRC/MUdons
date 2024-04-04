using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Mascari4615
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class CameraActive : MBase
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] private Image[] buttonUIImages;
        [SerializeField] private TextMeshProUGUI[] buttonUITexts;
        [SerializeField] private bool defaultActive;
        [SerializeField] private bool useCustomBool;

        private bool active;

        public bool Active
        {
            get => active;
            private set
            {
                active = value;
                OnActiveChange();
            }
        }

        private void Start()
        {
            if (useCustomBool == false)
                Active = defaultActive;

            OnActiveChange();
        }

        public void SetActive(bool targetActive)
        {
            if (DEBUG)
                MDebugLog($"{nameof(SetActive)}({targetActive})");

            Active = targetActive;
        }

        public void ToggleActive()
        {
            SetActive(!Active);
        }

        public void SetActiveTrue()
        {
            SetActive(true);
        }

        public void SetActiveFalse()
        {
            SetActive(false);
        }

        private void OnActiveChange()
        {
            if (DEBUG)
                MDebugLog($"{nameof(OnActiveChange)}");

            foreach (var i in buttonUIImages)
                i.color = GetGreenOrRed(Active);

            foreach (var t in buttonUITexts)
                t.color = GetGreenOrRed(Active);

            foreach (var camera in cameras)
                camera.enabled = Active;
        }
    }
}