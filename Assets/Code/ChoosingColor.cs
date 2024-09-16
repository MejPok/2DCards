using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosingColor : MonoBehaviour
{
    public GameObject colorpad;
    public void ChooseColor(int number){
        String[] colors = {"Brown", "Green", "Red", "Yellow"};
        String chosenColor = colors[number];
        GameState.colorOnTable = chosenColor;
        GameState.gs.nextTurn();
        
        
    }
    public void ColorPad(){
        colorpad.SetActive(!GameState.gs.choosingcColorAlready);
        GameState.gs.choosingcColorAlready = false;
    }
}
