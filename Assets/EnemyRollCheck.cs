using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRollCheck : MonoBehaviour
{
    public EnemyBase enemy;
    public GameObject Wall;
    public bool isRolled;

    private GameObject enemyorigin;
    private Vector3 enemyPos;
    private Quaternion enemyRot;

    public GameObject enemyprefab;
    bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        isRolled = false;

        enemyorigin = GameObject.FindGameObjectWithTag("enemy3");
        enemyPos = enemyorigin.gameObject.transform.position;
        enemyRot = enemyorigin.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyorigin == null && isSpawn == false && enemy.roll == false)
        {
            isSpawn = true;
            StartCoroutine(RespawnEnemy());
        }
        if (enemy.roll == true)
        {
            isRolled = true;
        }
        if (isRolled == true)
        {
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
            pb.tag = "enemy3";
            enemyorigin = pb;
            enemy = enemyorigin.GetComponent<EnemyBase>();

        }
        isSpawn = false;
    }
}
