using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{

    protected override void Start()
    {
        base.Start();
        stunCooldown = 3;
    }

    protected override void Update()
    {
        
        rot = this.transform.rotation;
        rot.y = rot.y * -180;

        if (canSeePlayer && rgd.State != RagdollEnemyAdvanced.RagdollState.Ragdolled && rgd.State != RagdollEnemyAdvanced.RagdollState.WaitStablePosition)
        {
            transform.LookAt(playerRef.transform);
            if (isStun == false)
            {
                timeBetweenShot = baseTimeBetweenShot;
                Shoot();
            }
            else if (isStun == true)
            {
                animator.SetBool("Dizzy", true);
                navMeshAgent.speed = 0;
                timeBetweenShot = 100000000;
                Invoke(nameof(resetStun), stunCooldown);
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

        if (isStun == true)
        {
            Debug.Log(isStun);
            animator.SetBool("Dizzy", true);
            navMeshAgent.speed = 0;
            Invoke(nameof(resetStun), stunCooldown);

        }

        if (health <= 0)
        {
            navMeshAgent.isStopped = true;
        }
        Physics.IgnoreLayerCollision(10, 20, true);

        Debug.Log(isStun);

    }
   

}
