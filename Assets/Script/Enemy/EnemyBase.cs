using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class EnemyBase : MonoBehaviour
{
    // Calling variable here
    #region Variables
    [Header("Base Stat")]
    [SerializeField]
    protected string name;
    public float maxHealth = 100f;
    [SerializeField]
    public int scorePoint = 10;
    public Score score;
    [HideInInspector] public float health;
    public int NAVspeed = 2;
    public float TimeToDelete;
    public bool dead;
    public bool roll;
    public bool slow;
    public bool full;
    public bool isHeadshot;
    public Text HeadshotText;



    [Header("FOV")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    

    [Header("References")]
    public GameObject playerRef; // DO NOT DEFINE / DRAG ANYTHING TO THIS
    [SerializeField] protected Animator animator = null;
    protected RagdollEnemyAdvanced rgd;
    public AudioSource ShootAudio;
    public NavMeshAgent navMeshAgent;
    public Vector3 bulletRecord;
    private ThirdPersonController TPS;
    public GameObject FloatingTextPrefab;
    protected Quaternion rot;

    [Header("Shooting")]
    public GameObject bullet;
    [Range(0, 2)]
    public float inaccuracy;
    public float timeBetweenShot = 0.5f; //  more value means low fire rate
    protected float baseTimeBetweenShot;
    protected float nextShot;
    // Adjust shooting point
    [Range(0, 2)]
    public float upward;
    [Range(0, 1)]
    public float forward;
    [Range(-1, 1)]
    public float leftright;
    public AudioSource KillAudio;
    public AudioSource HSAudio;
    public AudioSource PartialCombo;
    public AudioSource FullCombo;

    [Header("Stun")]
    protected bool isStun;
    public float stunCooldown = 5f;
    public AudioSource StunAudio;
    public bool stun;




    #endregion //Calling Var 
    private void Awake()
    {
        health = maxHealth;
    }

    protected virtual void Start()
    {
        dead = false;
        isStun = false;

        playerRef = GameObject.FindGameObjectWithTag("Player");
        TPS = playerRef.GetComponent<ThirdPersonController>();
        StartCoroutine(FOVRoutine());

        baseTimeBetweenShot = timeBetweenShot;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        rgd = GetComponent<RagdollEnemyAdvanced>();

        bulletRecord = Vector3.zero;
        rgd.State = RagdollEnemyAdvanced.RagdollState.Animated;
    }
    protected virtual void Update()
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

            if (health > 0)
            {
                navMeshAgent.isStopped = true;
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
        
    }
    // Ray Cast System 
    #region RayCast
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            //for(int i = 0; i <= rangeChecks.Length; i++)
            //{
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = false;
                }
                else
                {
                    canSeePlayer = true;
                }
            }
            else
            {
                canSeePlayer = false;
            }
            //}

            // If you want to check multiple objects inside the layer targetMask, do a for loop of rangeChecks

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }

    }
    #endregion //RayCast System

    // Shooting System
    #region Shooting
    public virtual void Shoot()
    {

        float randomNumberX = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberY = Random.Range(-inaccuracy, inaccuracy);
        float randomNumberZ = Random.Range(-inaccuracy, inaccuracy);

        if (Time.time >= nextShot)
        {
            GameObject clonebullet = Instantiate(bullet, transform.position + transform.forward * forward + transform.up * upward + transform.right * leftright, transform.rotation) as GameObject;
            clonebullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            ShootAudio.Play();
            Destroy(clonebullet, 5f);
            nextShot = Time.time + timeBetweenShot;
        }
    }
    #endregion


    public void takeDamage(float amount)
    {
        // Trigger Floating Text
        if (FloatingTextPrefab && health > 0)
        {
            ShowFloatingText();
        }
        

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected void ShowFloatingText()
    {
        GameObject go = Instantiate(FloatingTextPrefab, transform.position, rot, transform);
        go.GetComponent<TextMesh>().text = scorePoint.ToString();  //Change value of text in here: score.value.ToString();
    }
    protected void resetStun()
    {
        animator.SetBool("Dizzy", false);
        isStun = false;
        navMeshAgent.speed = NAVspeed;
        scorePoint = scorePoint - 5;
        
    }
    protected void Die()
    {
        dead = true;
        if (isHeadshot == true)
        {
            HSAudio.Play();
        }
        else if (isHeadshot == false)
        {
            KillAudio.Play();
        }
        timeBetweenShot = 1000000;
        if (TPS.IsRoll == false && TPS.IsSlow == false)
        {
            score.value += scorePoint;
            TPS.Health = TPS.Health + 10;
        }
        else if (TPS.IsRoll == true && TPS.IsSlow == true)
        {
            full = true;
            FullCombo.Play();
            score.value += scorePoint * 4;
            TPS.Health = TPS.Health + 20;
        }
        else if (TPS.IsRoll == true || TPS.IsSlow == true)
        {
            PartialCombo.Play();
            score.value += scorePoint * 2;
            TPS.Health = TPS.Health + 15;
        }
        
        if (TPS.IsRoll == true)
        {
            roll = true;
        }
        if (TPS.IsSlow == true)
        {
            slow = true;
        }



        rgd.State = RagdollEnemyAdvanced.RagdollState.Ragdolled;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, TimeToDelete);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //obstructionMask = LayerMask.GetMask("Default");
            takeDamage(30f);
            canSeePlayer = true;
            radius = 35f;
            bulletRecord = collision.transform.position;


        }
        if (collision.gameObject.tag == "Enemy")
        {
            rgd.State = RagdollEnemyAdvanced.RagdollState.Ragdolled;
            rgd.RagdollStatesController();
        }
        if (collision.gameObject.tag == "Throwing")
        {
            
            isStun = true;
            animator.SetTrigger("IsStun");
            Debug.Log("Throw hit");
            stun = true;
            score.value += 10;
            scorePoint = scorePoint + 5;
            StunAudio.Play();
            TPS.SlowedTime = TPS.SlowedTime + 2.5f;
            if (TPS.SlowedTime > 5f)
            {
                TPS.SlowedTime = 5f;
            }

            TPS.Health = TPS.Health + 5;
        }

    }
   
}
