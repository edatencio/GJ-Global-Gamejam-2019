using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private RPGTalk rpgTalkFirstPickup;
     [SerializeField] private RPGTalk rpgTalkRandom;
     [SerializeField] private float delay;

     private List<string> pedestriansPikedup;
     private Timer timer;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          pedestriansPikedup = new List<string>();
          timer = new Timer();
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (timer.isRunning && timer.ElapsedSeconds > delay)
          {
               timer.Stop();
               rpgTalkFirstPickup.showWithDialog[0].gameObject.SetActive(false);
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     public void Talk(Name name)
     {
          timer.Start();

          //if (pedestriansPikedup.Count == 0)
          {
               rpgTalkFirstPickup.NewTalk(string.Concat(name.ToString(), "-begin"), string.Concat(name.ToString(), "-end"));
               pedestriansPikedup.Add(name.ToString());
          }

          //else
          {
               //rpgTalkRandom.NewTalk(string.Concat(name.ToString(), "-begin"), string.Concat(name.ToString(), "-end"));
               //pedestriansPikedup.Add(name.ToString());
          }
     }
}
