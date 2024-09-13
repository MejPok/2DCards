using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetter : MonoBehaviour
{
    public PlayerInventory pi;
    public OpponentInventory pa;
    public bool isplayer; //0 enemy 1 player
    private float startX;
    int cardCount;
    public float StartX{
        get{return startX;}
        set{
            if(value <= -1.5f){
                startX = -1.5f;
            } else if( value >= 1.5f){
                startX = 1.5f;
            } else {
                startX = value;
            }
        }
    }

    public void Start(){
        if(isplayer){
            pi = GetComponent<PlayerInventory>();
        } else {
            pa = GetComponent<OpponentInventory>();
        }
        
        
    }

    void Update()
    {
        //min x value -1.50 max 1.50
        if(isplayer){
            cardCount = pi.currentCards.Count;
        } else {
            cardCount = pa.currentCards.Count;
        }

        float gap = 3f / (cardCount);
        float StartX = (float)(cardCount / 2f);

        if(isplayer){
            for(int i = 1; i < cardCount + 1; i++){
            StartX = gap * i - 1.5f;
            pi.currentCards[i - 1].transform.position = new Vector3(StartX, pi.currentCards[i - 1].transform.position.y , i * 0.01f);
            Debug.Log(StartX);
            }
        } else {
            for(int i = 1; i < cardCount + 1; i++){
            StartX = gap * i - 1.5f;
            pa.currentCards[i - 1].transform.position = new Vector3(StartX, pa.currentCards[i - 1].transform.position.y , i * 0.01f);
            Debug.Log(StartX);
        }
        }
        
    }
}
