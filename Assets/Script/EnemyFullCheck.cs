using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyFullCheck : MonoBehaviour
{
    public EnemyBase enemy;
    public bool isFull;
    public GameObject endSound;
    public float EndTime = 15;

    private GameObject enemyorigin;
    private Vector3 enemyPos;
    private Quaternion enemyRot;

    public GameObject enemyprefab;
    bool isSpawn = false;

    public GameObject Point;

    // Start is called before the first frame update
    void Start()
    {
        endSound.gameObject.SetActive(false);
        isFull = false;
        enemyorigin = GameObject.FindGameObjectWithTag("enemy5");
        enemyPos = enemyorigin.gameObject.transform.position;
        enemyRot = enemyorigin.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyorigin == null && isSpawn == false && enemy.full == false)
        {
            isSpawn = true;
            StartCoroutine(RespawnEnemy());
        }
        if (enemy.full == true)
        {
            isFull = true;
        
        }
        if (isFull == true)
        {
            Point.SetActive(true);
            endSound.gameObject.SetActive(true);
            Invoke(nameof(changeLevel), 10);
        }
        else
        {
            endSound.gameObject.SetActive(false);
        }
        
        
    }
 
    void changeLevel()
    {
        SceneManager.LoadScene("GameLevel_v4");
    }
    IEnumerator RespawnEnemy()
    {
        if (isSpawn)
        {
            int respawnTime = 2;
            yield return new WaitForSeconds(respawnTime);
            GameObject pb = Instantiate(enemyprefab, enemyPos, enemyRot);
            pb.tag = "enemy4";
            enemyorigin = pb;
            enemy = enemyorigin.GetComponent<EnemyBase>();

        }
        isSpawn = false;
    }
}
