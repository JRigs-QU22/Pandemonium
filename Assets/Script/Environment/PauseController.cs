using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState currentGameState = GameStateManager.Instance.CurrentGameState;
            GameState newGameState = currentGameState == GameState.Gameplay
                ?GameState.Paused
                :GameState.Gameplay;

            GameStateManager.Instance.SetState(newGameState);
            if(newGameState == GameState.Paused)
            {
                pausePanel.SetActive(true);
                GameObject[] arr = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(GameObject enemy in arr)
                {
                    Debug.Log(enemy.name);
                    enemy.GetComponent<EnemyBase>().navMeshAgent.speed = 0;

                }
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                GameObject[] arr = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in arr)
                {
                    enemy.GetComponent<EnemyBase>().navMeshAgent.speed = 2;

                }
                pausePanel.SetActive(false);
            }
            

        }
    }
    
}
