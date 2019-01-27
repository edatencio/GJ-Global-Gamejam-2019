using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class EnemyAlien : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private NavMeshAgent agent;
     [SerializeField] private Vector3Asset playerPosition;
     [SerializeField] private Animator animator;
     [SerializeField] private Rigidbody rBody;
     [SerializeField] private GameEvent gameOver;
     [SerializeField] private ParticleSystem deathParticle;

     private Vector3 currentPlayerPosition;

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
          if (Vector3.Distance(currentPlayerPosition, playerPosition.Value) > 0.5f)
          {
               agent.destination = playerPosition.Value;
               currentPlayerPosition = playerPosition.Value;

               animator.SetFloat("Velocity", Mathf.Abs(rBody.velocity.magnitude));
          }
     }

     /*************************************************************************************************
     *** OnTriggerEnter
     *************************************************************************************************/
     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == Constants.Player)
          {
               Death();
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     [Button]
     private void Death()
     {
          gameOver.Raise();
          deathParticle.gameObject.transform.SetParent(null);
          deathParticle.gameObject.SetActive(true);
          Destroy(gameObject);
     }
}
