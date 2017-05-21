using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Original code is from Realism Overhaul
// https://github.com/KSP-RO/RealismOverhaul/blob/master/Source/CoMShifter.cs

// Original RO notes was:
// this code is by asmi
// used with permission

namespace DescentMode
{
	class CoMShifter : PartModule
	{
		[KSPField]
		public Vector3 DescentModeCoM = new Vector3(0f, 0f, 0f);

		[KSPField]
		public string comString;

		protected bool loadedCoM = false;

		[KSPField(isPersistant = true)]
		public bool IsDescentMode;

		protected Vector3 _defaultCoM;

		[KSPEvent(guiName = "Turn Descent Mode On", guiActive = true, guiActiveEditor = true)]
		public void ToggleMode()
		{
			IsDescentMode = !IsDescentMode;
			SetDescentMode(IsDescentMode);
		}

		private void SetDescentMode(bool isOn)
		{
			if (isOn)
			{
				part.CoMOffset = DescentModeCoM + _defaultCoM;
				Events["ToggleMode"].guiName = "Turn Descent Mode Off";
			}
			else
			{
				part.CoMOffset = _defaultCoM;
				Events["ToggleMode"].guiName = "Turn Descent Mode On";
			}
			Fields["comString"].guiActive = PhysicsGlobals.ThermalDataDisplay;
			comString = part.CoMOffset.x.ToString("N3") + "," + part.CoMOffset.y.ToString("N3") + "," + part.CoMOffset.z.ToString("N3");
		}

		[KSPAction("Toggle Descent Mode")]
		public void Toggle(KSPActionParam param)
		{
			ToggleMode();
		}

		public override void OnAwake()
		{
			base.OnAwake();
			if (!loadedCoM)
			{
				_defaultCoM = part.CoMOffset;
				loadedCoM = true;
			}
		}

		public override void OnStart(StartState state)
		{
			base.OnStart(state);
			if (!HighLogic.LoadedSceneIsFlight)
				return;
			SetDescentMode(IsDescentMode);
		}
	}
}
