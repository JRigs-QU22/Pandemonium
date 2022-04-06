using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public AudioSource CPAudio;
   
    // Indicate if the checkpoint is activated
    public bool Activated = false;

    // List with all checkpoints objects in the scene
    public static List<GameObject> CheckPointsList;

    public EnemyBase[] enemies;

    // Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition()
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                    
                }
            }
        }
        return result;
    }

    private void ActivateCheckPoint()
    {
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().Activated = false;
        }

        // We activated the current checkpoint
        Activated = true;
        CPAudio.Play();
        Debug.Log("Active New Check Point");
    }

    void Start()
    {
        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
        
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].navMeshAgent.speed = 0;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "Player" && Activated == false)
        {
            ActivateCheckPoint();
            
            
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].navMeshAgent.speed = enemies[i].NAVspeed;
        }
    }

}