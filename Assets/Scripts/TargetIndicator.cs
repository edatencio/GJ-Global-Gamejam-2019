using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class TargetIndicator : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField, BoxGroup("Settings"), Range(0, 100)] private float edgeBuffer;
     [SerializeField, BoxGroup("Settings")] private Vector3 targetIconScale = Vector3.one;
     [SerializeField, BoxGroup("Settings")] private bool pointTarget = true;
     [SerializeField, BoxGroup("Settings")] private bool showDebugLines;

     [SerializeField, BoxGroup("Sprites")] private ParticleSystem targetParticleSystem;
     [SerializeField, BoxGroup("Sprites")] private Sprite targetIconOnScreen;
     [SerializeField, BoxGroup("Sprites")] private Sprite targetIconOffScreen;

     [SerializeField, BoxGroup("References")] private Camera mainCamera;
     [SerializeField, BoxGroup("References")] private Canvas canvas;

     private RectTransform icon;
     private Image iconImage;

     //Indicates if the object is out of the screen
     private bool outOfScreen;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          InstainateTargetIcon();
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (showDebugLines)
               DrawDebugLines();
          UpdateTargetIconPosition();
     }

     /*************************************************************************************************
     *** OnDisable
     *************************************************************************************************/
     private void OnDisable()
     {
          if (icon.gameObject != null)
               Destroy(icon.gameObject);
     }

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     private void InstainateTargetIcon()
     {
          icon = new GameObject().AddComponent<RectTransform>();
          icon.transform.SetParent(canvas.transform);
          icon.localScale = targetIconScale;
          icon.name = name + ": OTI icon";
          iconImage = icon.gameObject.AddComponent<Image>();
          iconImage.sprite = targetIconOnScreen;
     }

     private void UpdateTargetIconPosition()
     {
          Vector3 newPos = transform.position;
          newPos = mainCamera.WorldToViewportPoint(newPos);

          //Simple check if the target object is out of the screen or inside
          if (newPos.x > 1 || newPos.y > 1 || newPos.x < 0 || newPos.y < 0)
               outOfScreen = true;
          else
               outOfScreen = false;
          if (newPos.z < 0)
          {
               newPos.x = 1f - newPos.x;
               newPos.y = 1f - newPos.y;
               newPos.z = 0;
               newPos = Vector3Maxamize(newPos);
          }
          newPos = mainCamera.ViewportToScreenPoint(newPos);
          newPos.x = Mathf.Clamp(newPos.x, edgeBuffer, Screen.width - edgeBuffer);
          newPos.y = Mathf.Clamp(newPos.y, edgeBuffer, Screen.height - edgeBuffer);
          icon.transform.position = newPos;

          //Operations if the object is out of the screen
          if (outOfScreen)
          {
               //Show the target off screen icon
               iconImage.sprite = targetIconOffScreen;

               if (targetParticleSystem.isPlaying)
                    targetParticleSystem.Stop(true);

               if (pointTarget)
               {
                    //Rotate the sprite towards the target object
                    var targetPosLocal = mainCamera.transform.InverseTransformPoint(transform.position);
                    var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;

                    //Apply rotation
                    icon.transform.eulerAngles = new Vector3(0, 0, targetAngle);
               }
          }
          else
          {
               //Reset rotation to zero and swap the sprite to the "on screen" one
               icon.transform.eulerAngles = new Vector3(0, 0, 0);
               iconImage.sprite = targetIconOnScreen;

               if (!targetParticleSystem.isPlaying)
               {
                    targetParticleSystem.time = 0f;
                    targetParticleSystem.Play(true);
               }
          }
     }

     public void DrawDebugLines()
     {
          Vector3 directionFromCamera = transform.position - mainCamera.transform.position;
          Vector3 cameraForwad = mainCamera.transform.forward;
          Vector3 cameraRight = mainCamera.transform.right;
          Vector3 cameraUp = mainCamera.transform.up;
          cameraForwad *= Vector3.Dot(cameraForwad, directionFromCamera);
          cameraRight *= Vector3.Dot(cameraRight, directionFromCamera);
          cameraUp *= Vector3.Dot(cameraUp, directionFromCamera);
          Debug.DrawRay(mainCamera.transform.position, directionFromCamera, Color.magenta);
          Vector3 forwardPlaneCenter = mainCamera.transform.position + cameraForwad;
          Debug.DrawLine(mainCamera.transform.position, forwardPlaneCenter, Color.blue);
          Debug.DrawLine(forwardPlaneCenter, forwardPlaneCenter + cameraUp, Color.green);
          Debug.DrawLine(forwardPlaneCenter, forwardPlaneCenter + cameraRight, Color.red);
     }

     public Vector3 Vector3Maxamize(Vector3 vector)
     {
          Vector3 returnVector = vector;
          float max = 0;
          max = vector.x > max ? vector.x : max;
          max = vector.y > max ? vector.y : max;
          max = vector.z > max ? vector.z : max;
          returnVector /= max;
          return returnVector;
     }
}
