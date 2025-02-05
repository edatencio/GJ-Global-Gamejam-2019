/// <summary>
/// SURGE FRAMEWORK
/// Author: Bob Berkebile
/// Email: bobb@pixelplacement.com
/// 
/// A set of globally accessible Editor utilities.
/// 
/// </summary>

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Surge
{
	public class EditorUtilities : Editor
	{
		#region Public Methods
		/// <summary>
		/// Global error for the Editor.
		/// </summary>
		/// <param name="errorMessage">Error message.</param>
		public static void Error (string errorMessage)
		{
			EditorUtility.DisplayDialog ("Framework Error", errorMessage, "OK");
		}
		#endregion
	}
}
#endif