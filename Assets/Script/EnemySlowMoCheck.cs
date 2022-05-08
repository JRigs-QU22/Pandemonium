using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowMoCheck : MonoBehaviour
{
    public EnemyBase enemy;
    public GameObject Wall;
    public bool isSlowed;
    public AudioSource Chime;

    private GameObject enemyorigin;
    private Vector3 enemyPos;
    private Quaternion enemyRot;

    public GameObject enemyprefab;
    bool isSpawn = false;

    public GameObject Point;
    // Start is called before the first frame update
    void Start()
    {
        isSlowed = false;
        enemyorigin = GameObject.FindGameObjectWithTag("enemy4");
        enemyPos = enemyorigin.gameObject.transform.position;
        enemyRot = enemyorigin.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyorigin == null && isSpawn == false && enemy.slow == false)
        {
            isSpawn = true;
            StartCoroutine(RespawnEnemy());
        }
        if (enemy.slow == true)
        {
            isSlowed = true;
        }
        if (isSlowed == true)
        {
            
            Point.SetActive(true);
            Chime.Play();
            Wall.SetActive(false);
        }
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
