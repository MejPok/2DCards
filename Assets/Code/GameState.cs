using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public CardCounter cc;
    public PlayerInventory player;
    public OpponentInventory opponent;

    public void Start(){
        gs = this;
    }
    public void StartNow(){
        for(int i = 0; i < 6; i++){
            DrawCardTo("player");
        }
    }

    public void DrawCardTo(string who){
        GameObject newcard = Instantiate(cc.newValidCard());

        if(who == "player"){
            newcard.transform.position = new Vector3(0, -3, 0);
            player.currentCards.Add(newcard);
        } else {
            opponent.currentCards.Add(newcard);
        }
    }
}
