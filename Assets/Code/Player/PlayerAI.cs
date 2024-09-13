using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAI : MonoBehaviour
{
    public void DrawCard(){
        if(GameState.addOn == 0 && GameState.Stand == false){
            GameState.gs.DrawCardTo("player");
            GameState.gs.nextTurn();
        }
    }

    public void PlayCard(GameObject card){
        if(GameState.gs.PlayableCard(card) && GameState.turn == 1){
            GameState.gs.CardPlayed(card);
        }
    }

    public void CheckForDraws(){
        if(GameState.addOn > 0){
            foreach(GameObject card in GameState.gs.playerInv.currentCards){
                if(GameState.gs.PlayableCard(card)){
                    return;
                }
            }
            GameState.gs.DrawAddOns(true);
        }
    } 

    public void CheckForStands(){
        if(GameState.Stand == true){
            foreach(GameObject card in GameState.gs.playerInv.currentCards){
                if(GameState.gs.PlayableCard(card)){
                    return;
                }
            }
            GameState.gs.StandOff();
            GameState.gs.nextTurn();
        }
    }


}
