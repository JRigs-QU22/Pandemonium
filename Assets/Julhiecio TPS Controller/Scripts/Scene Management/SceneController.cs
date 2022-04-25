using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("JU TPS/Scene Management/Scene Controller")]
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// SceneController inside the Game Management
    /// Require a empty gameObject with tag "SpawnPoint"
    /// </summary>
    ThirdPersonController pl;
    public bool ResetLevelWhenPlayerDie;
    public float SecondsToReset = 4;
    public bool ExitGameWhenPressEsc;
    public bool ResetLevelWhenPressP;
    public GameObject data;
    void Start()
    {  
        pl = FindObjectOfType<ThirdPersonController>();
        pl.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        pl.transform.rotation = GameObject.FindGameObjectWithTag("SpawnPoint").transform.rotation;
        Destroy(GameObject.FindGameObjectWithTag("SpawnPoint"));
        

    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.P) && ResetLevelWhenPressP == true)
        {
            ResetLevel();
        }
        if(pl.IsDead == true && IsInvoking("ResetLevel") == false && ResetLevelWhenPlayerDie == true)
        {
            GameObject cpdata = Instantiate(data, CheckPoint.GetActiveCheckPointPosition(), Quaternion.identity) as GameObject;
            DontDestroyOnLoad(cpdata);
            Invoke("ResetLevel", SecondsToReset);
        }
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
}
