using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankAndScore_UI : MonoBehaviour
{
    public Score score;
    public ComboCounter CC;
    int PlayerScore;
    public Text ScoreText;
    public Text RankText;
    public Text TipText;
    // Start is called before the first frame update
    void Start()
    {
        TipText.enabled = false;
        PlayerScore = score.value;
        ScoreText.text = "Your Score: " + PlayerScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (CC.value < 1)
        {
            RankText.text = "Your Rank: F";
            TipText.enabled = true;
        }
        if (CC.value >= 1  && CC.value < 3)
        {
            RankText.text = "Your Rank: D";
            TipText.enabled = true;
        }
        if (CC.value >= 3  && CC.value <= 5)
        {
            RankText.text = "Your Rank: C";
            TipText.enabled = true;
        }
        if (CC.value > 5 && CC.value < 7)
        {
            RankText.text = "Your Rank: B";
            TipText.enabled = true;
        }
        if (CC.value >= 7 && CC.value <= 9)
        {
            RankText.text = "Your Rank: A";
            TipText.enabled = true;
        }
        if (CC.value > 9)
        {
            RankText.text = "Your Rank: S";
            TipText.enabled = false;
        }
    }
}
