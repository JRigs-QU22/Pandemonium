using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAudioStop : MonoBehaviour
{
    public AudioSource LastAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "Player")
        {
            LastAudio.Stop();
        }
    }
    

}

