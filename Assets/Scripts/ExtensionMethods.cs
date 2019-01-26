using UnityEngine;
using System.Diagnostics;

public static class ExtensionMethods
{
     /*************************************************************************************************
     *** Vector3
     *************************************************************************************************/
     public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
     {
          return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
     }

     /*************************************************************************************************
     *** LayerMask
     *************************************************************************************************/
     public static int layer(this LayerMask original)
     {
          int layerNumber = 0;
          int layer = original.value;
          while (layer > 0)
          {
               layer >>= 1;
               layerNumber++;
          }
          return layerNumber - 1;
     }

     /*************************************************************************************************
     *** Bounds
     *************************************************************************************************/
     public static Vector3 RandomPositionInside(this Collider collider)
     {
          Vector3 newPosition = Vector3.zero;

          newPosition.x = Random.Range(collider.transform.position.x - collider.bounds.extents.x + (collider.transform.localScale.x * 0.5f), collider.transform.position.x + collider.bounds.extents.x - (collider.transform.localScale.x * 0.5f));
          newPosition.y = Random.Range(collider.transform.position.y - collider.bounds.extents.y + (collider.transform.localScale.y * 0.5f), collider.transform.position.y + collider.bounds.extents.y - (collider.transform.localScale.y * 0.5f));
          newPosition.z = Random.Range(collider.transform.position.z - collider.bounds.extents.z + (collider.transform.localScale.z * 0.5f), collider.transform.position.z + collider.bounds.extents.z - (collider.transform.localScale.z * 0.5f));

          return newPosition;
     }

     public static Vector3 RandomPositionOnSurface(this Collider collider)
     {
          return RandomPositionInside(collider).With(y: collider.transform.position.y + collider.bounds.extents.y - (collider.transform.localScale.y * 0.5f));
     }
}
