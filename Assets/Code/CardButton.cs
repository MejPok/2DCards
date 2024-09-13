using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardButton : MonoBehaviour
{

    public void OnMouseDown()
    {
        DealNewCard();
    }
    
    public void DealNewCard(){
        GameState.gs.DrawCardTo("player");
    }
}
