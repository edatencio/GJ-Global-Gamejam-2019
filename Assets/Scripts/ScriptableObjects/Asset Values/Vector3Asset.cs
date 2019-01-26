using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewVector3Asset", menuName = "Asset Values/Vector3")]
public class Vector3Asset : ScriptableObject
{
     [SerializeField, BoxGroup("Settings")] private Vector3 value;
     [SerializeField, BoxGroup("Settings")] private bool constant;

     public Vector3 Value
     {
          get { return value; }

          set
          {
               Vector3 val = value;

               if (!constant)
                    this.value = val;
          }
     }
}
