using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool choosingcColorAlready;
    public ChoosingColor ccolor;


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
            SceneManager.LoadScene(1);
        } else if(opponent.currentCards.Count == 0){
            SceneManager.LoadScene(2);
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
        turn = 1;
        Vector3 posi = new Vector3(-0.5f, 0f, 0f);
        GameObject prefab = cc.ch.cards[Random.Range(0, 32)];
        GameObject newcard = Instantiate(prefab);
        newcard.GetComponent<JumpCard>().starter = true;
        newcard.transform.position = posi;
        CardInfo info = newcard.GetComponent<CardInfo>();
        colorOnTable = info.Color;
        numberOnTable = info.Number;
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
        if(cardI.Number == 12){
            return true;
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
            if(cardI.Number == 12){
                if(turn == 0){

                    cc.ReturnTheCard(card);
                    ccolor.ChooseColor(ChooseColorForEnemyAdvantage());
                    Debug.Log($"Card with number {cardI.Number} and color {cardI.Color} was played");
                } else if(!choosingcColorAlready){

                    ccolor.ColorPad();
                    GameState.gs.cc.ReturnTheCard(card);
                    choosingcColorAlready = true;
                }
                return;
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
    public int ChooseColorForEnemyAdvantage(){
        Dictionary<string, int> map = new Dictionary<string, int>();

        foreach(GameObject card in opponent.currentCards){
            CardInfo cardI = card.GetComponent<CardInfo>();
            if(map.ContainsKey(cardI.Color)){
                map[cardI.Color] += 1;
            } else {
                map.Add(cardI.Color, 1);
            }
        }

        string largestColor = "";
        int largestCount = 0;

        foreach(KeyValuePair<string, int> entry in map){
            if(entry.Value > largestCount){
                largestCount = entry.Value;
                largestColor = entry.Key;
            }
        }
        Debug.Log(largestCount + largestColor);
        switch(largestColor){
            case "Brown":
                return 0;
            case "Green":
                return 1;
            case "Red":
                return 2;
            case "Yellow":
                return 3;
        }   
        return 0;
    }
}
