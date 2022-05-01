using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public Score score;
    public Text textScore;
    public ThirdPersonController tps;
    // Start is called before the first frame update
    void Start()
    {
        if (score.value != 0)
        {
            score.value = score.value / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + score.value;
        /*
        if (tps.IsDead == true)
        {
            score.value = score.value - 100;
        }
        */
        if (score.value <= 0)
        {
            score.value = 0;
        }
    }
}
