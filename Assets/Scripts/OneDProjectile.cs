using Unity.Mathematics;
using UnityEngine;

public class OneDProjectile : MonoBehaviour
{
    OneDPlayerMove odpm;
    bool checkCol = false;
    bool colliding = true;
    void Awake()
    {
        odpm = GameObject.FindGameObjectWithTag("Player").GetComponent<OneDPlayerMove>();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag != "Player" && (checkCol || col.gameObject.tag == "Wall"))
        {
            checkCol = false;
            odpm.Teleport(col.transform.position.x, Mathf.Sign(odpm.gameObject.transform.position.x - gameObject.transform.position.x), col.transform.localScale.x);
        }
        colliding = true;
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "PassThruWall")
        {
        checkCol = true;   
        }
        colliding = false;

    }

    void Update()
    {
        //print(checkCol);
        float diff = Mathf.Abs(odpm.gameObject.transform.position.x - transform.position.x)-((odpm.gameObject.transform.localScale.x + transform.localScale.x)/2);
        if(!colliding && diff > 0.1f)
        {
            //checkCol = true;
        }
    }
}
