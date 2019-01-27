using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField, BoxGroup("Settings")] private Vector3Asset playerPosition;
     [SerializeField, BoxGroup("Settings")] private ParticleSystem deathParticle;
     [SerializeField, BoxGroup("Settings")] private CarController m_Car; // the car controller we want to use
     [SerializeField, BoxGroup("Touch Controls")] private ButtonPressed rightButton;
     [SerializeField, BoxGroup("Touch Controls")] private ButtonPressed leftButton;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          deathParticle.gameObject.SetActive(false);
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          playerPosition.Value = transform.position;
     }

     /*************************************************************************************************
     *** FixedUpdate
     *************************************************************************************************/
     private void FixedUpdate()
     {
          // pass the input to the car!
          float horizontal = Input.GetAxis("Horizontal");

          if (rightButton.isPressed) horizontal = 1f;
          if (leftButton.isPressed) horizontal = -1f;

          m_Car.Move(horizontal, 1f, 1f, 0f);
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     public void Death()
     {
          deathParticle.gameObject.transform.SetParent(null);
          deathParticle.gameObject.SetActive(true);
          Destroy(gameObject);
     }
}
