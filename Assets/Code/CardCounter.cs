using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardCounter : MonoBehaviour
{
    public CardHolder ch;
    public Dictionary<GameObject, int> counter = new Dictionary<GameObject, int>();

    public void Start(){
        ch = GetComponent<CardHolder>();

        foreach(GameObject go in ch.cards){
            counter.Add(go, 0);
        }
        GameState.gs.StartNow();
    }

    public bool cardsAllOut(){
        foreach(GameObject key in counter.Keys){
            if(counter[key] == 0){
                return false;
            }
        }
        return true;
    }

    public GameObject newValidCard(){
        int random = Random.Range(0, 32);
        GameObject chosenOne = ch.cards[random];
        if(counter[chosenOne] == 0){
            counter[chosenOne] = 1;
            return chosenOne;
        } else {
            if(!cardsAllOut()){
                return newValidCard();
            } else {
                Debug.Log("No cards left");
            }
        }
        return null;
    }
}
