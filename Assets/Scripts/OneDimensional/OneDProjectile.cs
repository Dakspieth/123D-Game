using Unity.Mathematics;
using UnityEngine;

public class OneDProjectile : MonoBehaviour
{
    OneDPlayerMove odpm;
    bool checkCol = false;
    void Awake()
    {
        odpm = GameObject.FindGameObjectWithTag("Player").GetComponent<OneDPlayerMove>();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player" && ((checkCol && col.gameObject.tag == "PassThruWall") || col.gameObject.tag == "Wall"))
        {
            checkCol = false;
            odpm.Teleport(col.transform.position.x, Mathf.Sign(odpm.gameObject.transform.position.x - gameObject.transform.position.x), col.transform.localScale.x);
        } else if (col.gameObject.tag == "BorderRight" || col.gameObject.tag == "BorderLeft" || col.gameObject.tag == "NoTeleCol")
        {
            odpm.Teleport(odpm.gameObject.transform.position.x, 1, -odpm.gameObject.transform.localScale.x);
        }
    }

    void Update()
    {
        float diff = Mathf.Abs(odpm.gameObject.transform.position.x - transform.position.x)-((odpm.gameObject.transform.localScale.x + transform.localScale.x)/2);
        if(diff > 0.25f) // only starts checking for collisions after distance traveled
        {
            checkCol = true;
        }
    }
}
