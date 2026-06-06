using UnityEngine;
using System.Collections.Generic;

public class TeleporterItem : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    float randomWait;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(randomWait < 0)
        {
            randomWait = Random.Range(0.25f, 1f);
            sr.sprite = sprites[Random.Range(0, sprites.Count)];
        }
        randomWait -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            print("TelePorter Gained!");
            GameObject.FindGameObjectWithTag("Player").GetComponent<OneDPlayerMove>().haveShootItem = true;
            Destroy(gameObject);
        }
    }
}
