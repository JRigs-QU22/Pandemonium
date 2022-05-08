using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    public GameObject boss; // drag the boss on the inspector 
    public ThirdPersonController pl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check the boss 
        if (boss == null)
        {
            SceneManager.LoadScene("Win");
        }

        // UNCOMMENT THIS TO DEBUG

        /*if (Input.GetKeyDown(KeyCode.O))
        {
            pl.Health = 0;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Win");
        }*/
    }
}
