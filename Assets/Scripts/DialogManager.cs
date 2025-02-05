using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private RPGTalk rpgTalkFirstPickup;
     [SerializeField] private float delayToHide;

     public int pedestriansPiked;
     private Timer timerHide;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          timerHide = new Timer();
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (timerHide.isRunning && timerHide.ElapsedSeconds > delayToHide)
          {
               timerHide.Stop();
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
          timerHide.Start();
          rpgTalkFirstPickup.NewTalk(string.Concat(name.ToString(), "-begin"), string.Concat(name.ToString(), "-end"));
          pedestriansPiked++;
     }
}
