using UnityEngine;

public class Pedestrian : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private ParticleSystem pickupParticleSystem;
     [SerializeField] private Vector3Asset playerPosition;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          pickupParticleSystem.gameObject.SetActive(false);
     }

     /*************************************************************************************************
     *** LateUpdate
     *************************************************************************************************/
     private void LateUpdate()
     {
          transform.rotation = Quaternion.LookRotation((playerPosition.Value - transform.position).With(y: 0f));
          Debug.DrawRay(transform.position, playerPosition.Value - transform.position, Color.red);
     }

     /*************************************************************************************************
     *** OnTriggerEnter
     *************************************************************************************************/
     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == Constants.Player)
          {
               pickupParticleSystem.transform.SetParent(null);
               pickupParticleSystem.gameObject.SetActive(true);
               Destroy(gameObject);
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
}
