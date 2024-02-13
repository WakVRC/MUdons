
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Mascari4615
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class MTargetBool : CustomBool
	{
		[Header("_" + nameof(MTargetBool))]
		[SerializeField] private MTarget _mTarget;
		public MTarget MTarget => _mTarget;

		protected override void Start()
		{
			OnValueChange();
		}

		public void UpdateValue()
		{
			SetValue(MTarget.IsLocalPlayerTarget);
		}
	}
}