using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAudioStop : MonoBehaviour
{
    public AudioSource LastAudio;
    public AudioSource currentAudio;
    [SerializeField]
    private EnemyBase[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAudio.isPlaying)
        {
            stopAllAudio();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "Player")
        {
            LastAudio.Stop();
        }
    }
    void stopAllAudio()
    {
        try
        {
            foreach (EnemyBase enemy in enemies)
            {
                if (enemy.KillAudio.isPlaying) enemy.KillAudio.Stop();
                if (enemy.HSAudio.isPlaying) enemy.HSAudio.Stop();
                if (enemy.PartialCombo.isPlaying) enemy.PartialCombo.Stop();
                if (enemy.FullCombo.isPlaying) enemy.FullCombo.Stop();
            }
        }
        catch
        {

        }
            
    }

}

