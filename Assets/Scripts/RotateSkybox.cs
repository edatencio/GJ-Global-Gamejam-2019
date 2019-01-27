using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private float speed;
     [SerializeField] private float speedMultiplier = 0.2f;

     private float rotation;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          rotation += Time.deltaTime * speed * speedMultiplier;
          RenderSettings.skybox.SetFloat("_Rotation", rotation);
     }

     /*************************************************************************************************
     *** OnDisable
     *************************************************************************************************/
     private void OnDisable()
     {
          RenderSettings.skybox.SetFloat("_Rotation", 0f);
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
}
