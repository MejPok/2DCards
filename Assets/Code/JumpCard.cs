using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCard : MonoBehaviour
{
    public void OnMouseEnter()
    {
        Debug.Log("kripl");
        transform.position = new Vector2(transform.position.x, -2f);
    }

    public void OnMouseExit()
    {
        transform.position = new Vector2(transform.position.x, -3f);
    }
}
