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
	class ValueVector4 : TweenBase
	{
		#region Public Properties
		public Vector4 EndValue {get; private set;}
		#endregion

		#region Private Variables
		Action<Vector4> _valueUpdatedCallback;
		Vector4 _start;
		#endregion

		#region Constructor
		public ValueVector4 (Vector4 startValue, Vector4 endValue, Action<Vector4> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
		{
			//set essential properties:
			SetEssentials (Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

			//catalog custom properties:
			_valueUpdatedCallback = valueUpdatedCallback;
			_start = startValue;
			EndValue = endValue;
		}
		#endregion

		#region Processes
		protected override bool SetStartValue ()
		{
			return true;
		}

		protected override void Operation (float percentage)
		{
			Vector4 calculatedValue = TweenUtilities.LinearInterpolate (_start, EndValue, percentage);
			_valueUpdatedCallback (calculatedValue);
		}
		#endregion

		#region Loops
		public override void Loop ()
		{
			ResetStartTime ();
		}

		public override void PingPong ()
		{
			ResetStartTime ();
			Vector4 temp = _start;
			_start = EndValue;
			EndValue = temp;
		}
		#endregion
	}
}