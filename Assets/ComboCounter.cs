using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboCounter", menuName = "ScriptableObjects/ComboCounter", order = 2)]
public class ComboCounter : ScriptableObject
{
    public float initValue;
    public float value;


    private void OnEnable()
    {
        value = initValue;
    }
}
