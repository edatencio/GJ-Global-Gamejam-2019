using UnityEngine;
using NaughtyAttributes;
using Surge;

public class BuildingFade : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/

     //[SerializeField] private float fadeDuration;
     [SerializeField] private Renderer rend;
     [SerializeField] private Camera mainCamera;
     [SerializeField] private Vector3Asset playerPosition;

     private Collider thisCollider;
     private enum State { Opaque, Transparent }
     private State state;

     //private Surge.TweenSystem.TweenBase tween;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          thisCollider = GetComponent<Collider>();
          state = State.Transparent;
          Opaque();
     }

     /*************************************************************************************************
     *** FixedUpdate
     *************************************************************************************************/
     private void FixedUpdate()
     {
          RaycastHit hit;
          Vector3 direction = (playerPosition.Value - mainCamera.transform.position).normalized;
          Physics.Raycast(mainCamera.transform.position, direction, out hit);

          if (hit.collider.tag == Constants.Player)
               Opaque();
          else if (hit.collider == thisCollider)
               Transparent();
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     [Button]
     private void Transparent()
     {
          if (state != State.Transparent)
          {
               state = State.Transparent;
               Color color = rend.material.color;
               color.a = 0.2f;
               rend.material.SetColor("_Color", color);
               rend.material.ChangeRenderMode(StandardShaderUtils.BlendMode.Transparent);

               //if (tween != null && tween.Status == Tween.TweenStatus.Running)
               //     tween.Stop();
               //tween = Tween.Color(rend.material, color, fadeDuration, 0f, Tween.EaseInOut, Tween.LoopType.None, null, null, true);
          }
     }

     [Button]
     private void Opaque()
     {
          if (state != State.Opaque)
          {
               state = State.Opaque;

               Color color = rend.material.color;
               color.a = 1f;

               rend.material.SetColor("_Color", color);
               rend.material.ChangeRenderMode(StandardShaderUtils.BlendMode.Opaque);

               //if (tween != null && tween.Status == Tween.TweenStatus.Running)
               //     tween.Stop();
               //tween = Tween.Color(rend.material, color, fadeDuration, 0f, Tween.EaseInOut, Tween.LoopType.None, null, null, true);
          }
     }
}
