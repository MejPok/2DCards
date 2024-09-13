using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public CardCounter cc;
    public PlayerInventory playerInv;
    public OpponentInventory opponent;

    //
    public static int numberOnTable;
    public int num;
    public static string colorOnTable;
    public string col;

    //


    public PlayerAI player;
    public EnemyAI enemy;
    public static int turn; // player is 1
    public int t;
    public static int addOn;
    public int add;
    public static bool Stand;
    public bool stan;

    float decisiontimer;
    float randTimer;
    bool think;


    public void nextTurn(){
        turn++;
        if(turn == 1){
            player.CheckForDraws();
            player.CheckForStands();
        }
        if(turn > 1){
            turn = 0;
        }
        if(turn == 0){
            randTimer = Random.Range(0.2f, 3.5f);
            think = true;
        }
        
    }

    public void Update(){
        if(playerInv.currentCards.Count == 0){

        }

        col = colorOnTable;
        num = numberOnTable;
        t = turn;
        add = addOn;
        stan = Stand;

        if(think){
            decisiontimer += Time.deltaTime;
            if(decisiontimer > randTimer){
                think = false;
                decisiontimer = 0;
                enemy.MakeChoice();
            }
        }
    }
    public void Start(){
        gs = this;
        turn = 1;//Random.Range(0,2);

        numberOnTable = Random.Range(7,15);

        int rand = Random.Range(0, 4);
        if(rand == 0){
            colorOnTable = "Red";
        } else if(rand == 1){
            colorOnTable = "Yellow";
        } else if(rand == 2){
            colorOnTable = "Brown";
        } else if(rand == 3){
            colorOnTable = "Green";
        }
    }
    public void StartNow(){
        for(int i = 0; i < 4; i++){
            DrawCardTo("player");
            DrawCardTo("enemy");
        }
    }

    public void DrawCardTo(string who){
        GameObject newcard = Instantiate(cc.newValidCard());

        if(who == "player"){
            newcard.transform.position = new Vector3(0, -3, 0);
            playerInv.currentCards.Add(newcard);
            newcard.GetComponent<JumpCard>().playerCard = true;
        } else if(who == "enemy"){
            opponent.currentCards.Add(newcard);
        }
    }

    public void DrawAddOns(bool isplayer){
        if(isplayer){
            for(int i = 0; i < addOn; i++){
                DrawCardTo("player");
            }
        } else {
            for(int i = 0; i < addOn; i++){
                DrawCardTo("enemy");
            }
        }
        addOn = 0;
        nextTurn();
    }

    public bool PlayableCard(GameObject card){
        CardInfo cardI = card.GetComponent<CardInfo>();
        
        if(addOn > 0){
            return (cardI.Number == 7);
        }
        if(Stand == true){
            return (cardI.Number == 14);
        }

        return (cardI.Number == numberOnTable || cardI.Color == colorOnTable);

    }

    public void CardPlayed(GameObject card){
        CardInfo cardI = card.GetComponent<CardInfo>();
        if(PlayableCard(card)){
            numberOnTable = cardI.Number;
            colorOnTable = cardI.Color;
            if(cardI.Number == 7){
                addOn += 2;
                
            }
            if(cardI.Number == 14){
                Stand = true;
            }
        } else {
            Debug.Log("incorrect card");
        }
        cc.ReturnTheCard(card);
        Debug.Log($"Card with number {cardI.Number} and color {cardI.Color} was played");
        nextTurn();
    }
    public void StandOff(){
        Stand = false;
    }
}
