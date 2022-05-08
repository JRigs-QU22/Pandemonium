using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloAudio : MonoBehaviour
{
    public AudioSource SloIn;
    public AudioSource SloOut;
    public ThirdPersonController TPS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && TPS.SlowStart == false)
        {
            SloIn.Play();
        }
        if (TPS.IsSlow == true)
        {
            SloOut.Play();
        }
    }
}
