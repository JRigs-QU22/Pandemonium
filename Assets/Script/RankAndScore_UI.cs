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
        ScoreText.text = "Your Pay: $" + PlayerScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (CC.value < 1)
        {
            RankText.text = "Your Rank: F";
            TipText.enabled = true;
        }
        if (CC.value >= 1  && CC.value < 6)
        {
            RankText.text = "Your Rank: D";
            TipText.enabled = true;
        }
        if (CC.value >= 6  && CC.value <= 10)
        {
            RankText.text = "Your Rank: C";
            TipText.enabled = true;
        }
        if (CC.value > 10 && CC.value < 14 && PlayerScore >= 100 && PlayerScore <= 300)
        {
            RankText.text = "Your Rank: B ";
            TipText.enabled = true;
        }
        if (CC.value >= 14 && CC.value <= 18 && PlayerScore > 300 && PlayerScore < 500)
        {
            RankText.text = "Your Rank: A";
            TipText.enabled = true;
        }
        if (CC.value > 26 && PlayerScore >= 500)
        {
            RankText.text = "Your Rank: S";
            TipText.enabled = false;
        }
    }
}
