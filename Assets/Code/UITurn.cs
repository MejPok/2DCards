using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITurn : MonoBehaviour
{
   public TextMeshProUGUI text;
   public TextMeshProUGUI text1;

    void Update()
    {
        if(GameState.turn == 1){
            text1.text = "Player turn";
            text.text = "";
        } else {
            text.text = "Enemy turn";
            text1.text = "";
        }
    }
}
