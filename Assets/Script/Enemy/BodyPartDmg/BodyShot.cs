using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShot : MonoBehaviour
{
    public EnemyBase enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("Bodyshot");
            enemy.takeDamage(40f);
            enemy.canSeePlayer = true;
            enemy.radius = 35f;
        }
    }
}
