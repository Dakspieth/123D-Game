using UnityEngine;
using System;

public class RedWallKey : MonoBehaviour
{
    public static event Action unlockRedWall;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(unlockRedWall != null) unlockRedWall();
            Destroy(gameObject);
        }   
    }
}
