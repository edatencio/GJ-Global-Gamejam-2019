using UnityEngine;
using Cinemachine;
using NaughtyAttributes;

public class CinemachineCameraShake : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private CinemachineVirtualCamera virtualCamera;
     [SerializeField] private float duration;
     [SerializeField] private float aplitude;
     [SerializeField] private float frequency;
     [SerializeField] private bool shakeOnAwake;

     private CinemachineBasicMultiChannelPerlin noise;
     private Timer timer;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          timer = new Timer();
          noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

          if (shakeOnAwake)
               Shake();
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (timer.isRunning && timer.ElapsedSeconds > duration)
          {
               timer.Stop();
               Stop();
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/
     public bool isRunning
     {
          get { return timer.isRunning; }
     }

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     [Button]
     public void Shake()
     {
          noise.m_AmplitudeGain = aplitude;
          noise.m_FrequencyGain = frequency;
          timer.Start();
     }

     [Button]
     public void Stop()
     {
          noise.m_AmplitudeGain = 0f;
          noise.m_FrequencyGain = 0f;
          timer.Stop();
     }
}
