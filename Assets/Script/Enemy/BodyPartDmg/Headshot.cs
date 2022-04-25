using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Headshot : MonoBehaviour
{
    public EnemyBase enemy;
    public Text HeadshotText;
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
        if(collision.gameObject.tag == "Bullet")
        {
            enemy.isHeadshot = true;
            HeadshotText.enabled = true;
            //Debug.Log("Headshot");
            enemy.takeDamage(1000f);
            enemy.score.value += enemy.scorePoint * 2;
            enemy.canSeePlayer = true;
            enemy.radius = 35f;
        }
        else
        {
            HeadshotText.enabled = false;
        }
    }
}
