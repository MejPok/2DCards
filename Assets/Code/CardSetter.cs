using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetter : MonoBehaviour
{
    public PlayerInventory pi;

    public float rozdil;
    public float mezera;

    public void Start(){
        pi = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        float fullmezera = rozdil + mezera;

        float startX = (pi.currentCards.Count - 1) * -0.2f;
        foreach(GameObject go in pi.currentCards){
            go.transform.position = new Vector2(startX + fullmezera, transform.position.y);
            startX += (pi.currentCards.Count - 1) * 0.2f;
        }
    }
}
