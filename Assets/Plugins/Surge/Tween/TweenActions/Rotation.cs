/// <summary>
/// SURGE FRAMEWORK
/// Author: Bob Berkebile
/// Email: bobb@pixelplacement.com
/// </summary>

using UnityEngine;
using System;
using Surge;

namespace Surge.TweenSystem
{
	class Rotation : TweenBase
	{
		#region Public Properties
		public Vector3 EndValue {get; private set;}
		#endregion

		#region Private Variables
		Transform _target;
		Vector3 _start;
		#endregion

		#region Constructor
		public Rotation (Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
		{
			//set essential properties:
			SetEssentials (Tween.TweenType.Rotation, target.GetInstanceID (), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

			//catalog custom properties:
			_target = target;
			EndValue = endValue;
		}
		#endregion

		#region Processes
		protected override bool SetStartValue ()
		{
			if (_target == null) return false;
			_start = _target.eulerAngles;
			return true;
		}

		protected override void Operation (float percentage)
		{
			Quaternion calculatedValue = Quaternion.Euler (TweenUtilities.LinearInterpolateRotational (_start, EndValue, percentage));
			_target.rotation = calculatedValue;
		}
		#endregion

		#region Loops
		public override void Loop ()
		{
			ResetStartTime ();
			_target.eulerAngles = _start;
		}

		public override void PingPong ()
		{
			ResetStartTime ();
			_target.eulerAngles = EndValue;
			EndValue = _start;
			_start = _target.eulerAngles;
		}
		#endregion
	}
}