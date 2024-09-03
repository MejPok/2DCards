using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public int Number;
    public string Color;

    void Start()
    {
        gameObject.name = Color + Number;
    }

    
}
