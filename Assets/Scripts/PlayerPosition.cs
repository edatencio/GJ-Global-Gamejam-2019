using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private Vector3Asset playerPosition;

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
          playerPosition.Value = transform.position;
     }

     /*************************************************************************************************
     *** FixedUpdate
     *************************************************************************************************/
     private void FixedUpdate()
     {
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
}
