using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardCounter : MonoBehaviour
{
    public float currentZ;
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
        List<GameObject> chosenCards = new List<GameObject>();

        foreach(KeyValuePair<GameObject, int> entry in counter){
            if(entry.Value == 0){
                chosenCards.Add(entry.Key);
            }
        }
        int randomNumber = Random.Range(0, chosenCards.Count + 1);

        if(chosenCards.Count == 0){
            Debug.Log("No cards left ig");
            return null;
        }
        counter[chosenCards[randomNumber]]++;

        return chosenCards[randomNumber];
    }

    public void ReturnTheCard(GameObject card){
        PlayerInventory playerInv = GameState.gs.playerInv;
        OpponentInventory enemyInv = GameState.gs.opponent;
        string who = "";
        for(int i = 0; i < playerInv.currentCards.Count; i++){
            if(playerInv.currentCards[i] == card){
                playerInv.currentCards.Remove(card);
                card.GetComponent<JumpCard>().playerCard = false;
                moveTheCardToDeck(card);
                Debug.Log("card removed from player inv");
                counter[card] = 0;
                return;
            }
        }
        for(int i = 0; i < enemyInv.currentCards.Count; i++){
            if(enemyInv.currentCards[i] == card){
                enemyInv.currentCards.Remove(card);
                moveTheCardToDeck(card);
                Debug.Log("card removed from enemy inv");
                counter[card] = 0;
                return;
            }
        }

    }

    public void moveTheCardToDeck(GameObject card){
        currentZ -= 0.05f;
        float random = Random.Range(-0.1f, 0.1f);
        float random1 = Random.Range(-0.1f, 0.1f);
        float random2 = Random.Range(-60f, 60f);
        card.transform.position = new Vector3(-0.5f + random, 0f + random1, currentZ);
        card.transform.rotation = new Quaternion(0f, 0f, 0f, random2);
    }
}
