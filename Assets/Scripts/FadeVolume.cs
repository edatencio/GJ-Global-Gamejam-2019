using UnityEngine;
using NaughtyAttributes;
using Surge;
using UnityEngine.Video;

[RequireComponent(typeof(GameEventListener))]
public class FadeVolume : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField, MinValue(0f), BoxGroup] private float fadeTime;
     [SerializeField, MinValue(0f), BoxGroup] private float delay;

     [SerializeField, BoxGroup("Audio")] private bool useAudio;
     [SerializeField, BoxGroup("Audio")] private AudioSource audioSource;

     [SerializeField, BoxGroup("Video")] private bool useVideo;
     [SerializeField, BoxGroup("Video")] private VideoPlayer videoPlayer;

     private float timer;
     private bool delaySet;
     private bool once;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          timer = delay;
          once = true;

          if (!useAudio && !useVideo)
               Debug.LogWarning(string.Concat(name, ": Not volume value selected."));
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (delaySet)
          {
               timer -= Time.unscaledDeltaTime;

               if (timer <= 0)
                    ActuallyFadeOut();
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     [Button("Fade Out")]
     public void FadeOut()
     {
          if (delay > 0)
               delaySet = true;
          else
               ActuallyFadeOut();
     }

     private void ActuallyFadeOut()
     {
          if (once)
          {
               once = false;

               if (useAudio)
                    Tween.Volume(audioSource, 0f, fadeTime, 0f, Tween.EaseOut, Tween.LoopType.None,
                         null, null, false);

               if (useVideo)
                    Tween.Value(1f, 0f, (float val) => videoPlayer.SetDirectAudioVolume(0, val), fadeTime,
                         delay, Tween.EaseOut, Tween.LoopType.None, null, null, false);
          }
     }
}
