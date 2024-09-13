using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCard : MonoBehaviour
{
    public bool playerCard;
    float timer;
    int clicked;

    void Start(){
        if(playerCard){
            return;
        } else {
            transform.position = new Vector2(transform.position.x, 5f);
        }
    }
    void Update(){
        if(!playerCard){
            return;
        }
        if(clicked == 1){
            timer += Time.deltaTime;
            if(timer > 0.3f){
                clicked = 0;
                timer = 0f;
            }
            
        }
        if(clicked == 2){
            GameState.gs.player.PlayCard(this.gameObject);
            Debug.Log("card notified");
            clicked = 0;
        }
    }
    public void OnMouseEnter()
    {
        if(!playerCard){
            return;
        }
        transform.position = new Vector2(transform.position.x, -2f);
    }

    public void OnMouseExit()
    {
        if(!playerCard){
            return;
        }
        transform.position = new Vector2(transform.position.x, -3f);
    }

    public void OnMouseUp()
    {
        if(!playerCard){
            return;
        }
        clicked++;
    }

}
