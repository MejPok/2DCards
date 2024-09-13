using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //hrat kartu
    //liynout si
    //stat
    OpponentInventory inv;

    void Start(){
        inv = GetComponent<OpponentInventory>();
    }
    public void MakeChoice(){
        foreach(GameObject card in inv.currentCards){
            if(GameState.gs.PlayableCard(card))
            {
                GameState.gs.CardPlayed(card);
                return;
            }  
        }
        if(GameState.addOn == 0 && GameState.Stand == false)
            {
                GameState.gs.DrawCardTo("enemy");
                GameState.gs.nextTurn();
            }
            else if(GameState.Stand == true){
                GameState.gs.StandOff();
                GameState.gs.nextTurn();
            } else if(GameState.addOn > 0 && GameState.Stand == false){
                GameState.gs.DrawAddOns(false);
            }
    }



}
