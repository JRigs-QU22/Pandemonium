using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoRefill : MonoBehaviour
{
    public ThirdPersonController TPS;
    public AudioSource RefillAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TPS.SlowedTime <= 0f)
        {
            RefillAudio.Play();
            TPS.SlowedTime = 10f;
            
        }
    }
}
