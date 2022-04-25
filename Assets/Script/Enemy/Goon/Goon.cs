using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : EnemyBase
{

    protected override void Start()
    {
        base.Start();
       
    }

    protected override void Update()
    {
        base.Update();
        if (canSeePlayer)
        {
            animator.SetBool("Shoot", true);
            animator.SetBool("Run", false);
        } 
        else
        {
            animator.SetBool("Shoot", false);
        }
        try
        {
            Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Bullet").GetComponent<Collider>(), GetComponent<CapsuleCollider>());   
        }
        catch 
        {
            
        }
    }
    
}
