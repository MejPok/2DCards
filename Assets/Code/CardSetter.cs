using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetter : MonoBehaviour
{
    public PlayerInventory pi;

    private float startX;
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
        pi = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        //min x value -1.50 max 1.50

        int cardCount = pi.currentCards.Count;

        float gap = 3f / (cardCount);
        float StartX = (float)(cardCount / 2f);


        for(int i = 1; i < cardCount + 1; i++){
            StartX = gap * i - 1.5f;
            pi.currentCards[i - 1].transform.position = new Vector3(StartX, pi.currentCards[i - 1].transform.position.y , i * 0.01f);
            Debug.Log(StartX);
        }
    }
}
