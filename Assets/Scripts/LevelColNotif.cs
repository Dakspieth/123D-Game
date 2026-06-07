using UnityEngine;

public class LevelColNotif : MonoBehaviour
{
    public int levelNum;
    LevelLoader ll;
    void Start()
    {
        ll = transform.parent.GetComponent<LevelLoader>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            ll.LoadLevel(levelNum);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            ll.UnloadLevel(levelNum);
        }
    }
}
