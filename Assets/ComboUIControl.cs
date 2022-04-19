using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIControl : MonoBehaviour
{
    public Text RollText;
    public Text SlowText;
    public Text HeadshotText;
    public ThirdPersonController TPS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TPS.IsRoll == true)
        {
            RollText.enabled = true;
        }
        else
        {
            RollText.enabled = false;
        }
        
        if (TPS.IsSlow == true)
        {
            SlowText.enabled = true;
        }
        else
        {
            SlowText.enabled = false;
        }
    }
}
