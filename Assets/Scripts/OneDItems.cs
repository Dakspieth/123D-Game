using UnityEngine;
using System.Collections.Generic;

public class OneDItems : MonoBehaviour
{
    [Header("Teleporter")]
    public bool Teleporter;
    public List<Sprite> sprites = new List<Sprite>();

    [Header("RedWallKey")]
    public bool RedWallKey;

    void Start()
    {
        if(Teleporter)
        {
            gameObject.AddComponent<TeleporterItem>();
        }
        if(RedWallKey)
        {
            gameObject.AddComponent<RedWallKey>();
        }
    }

}
