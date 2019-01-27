using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
     /*************************************************************************************************
     *** Variables
     *************************************************************************************************/
     [SerializeField] private DialogManager dialogManager;
     [SerializeField] private PlayerController playerController;
     [SerializeField] private GameObject gameOverPanel;
     [SerializeField] private GameObject youWinPanel;
     [SerializeField] private GameObject buttons;

     private bool once;
     private bool gameOver;

     /*************************************************************************************************
     *** Start
     *************************************************************************************************/
     private void Start()
     {
          gameOverPanel.SetActive(false);
          youWinPanel.SetActive(false);
     }

     /*************************************************************************************************
     *** Update
     *************************************************************************************************/
     private void Update()
     {
          if (dialogManager.pedestriansPiked == 6 && !gameOverPanel.activeSelf && !youWinPanel.activeSelf)
          {
               youWinPanel.SetActive(true);
               buttons.SetActive(true);
               playerController.Stop();
               playerController.enabled = false;
          }

          if (gameOver && !youWinPanel.activeSelf && !gameOverPanel.activeSelf)
          {
               buttons.SetActive(true);
               gameOverPanel.SetActive(true);
          }
     }

     /*************************************************************************************************
     *** Properties
     *************************************************************************************************/

     /*************************************************************************************************
     *** Methods
     *************************************************************************************************/
     public void Done()
     {
          gameOver = transform;
     }

     public void TryAgain()
     {
          SceneManager.LoadScene(0);
     }

     public void Quit()
     {
          Application.Quit();
     }
}
