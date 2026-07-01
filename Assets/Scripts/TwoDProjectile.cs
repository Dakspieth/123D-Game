using UnityEngine;

public class TwoDProjectile : MonoBehaviour
{
TwoDPlayerTele tdpt;
float camPosX, camPosY, camSize;
void Awake() {
    tdpt = GameObject.FindGameObjectWithTag("Player").GetComponent<TwoDPlayerTele>();
    gameObject.SetActive(false);
    camSize = Camera.main.orthographicSize;
}

void Update() {
    camPosX = Camera.main.transform.position.x;
    camPosY = Camera.main.transform.position.y;
    if(Mathf.Abs(transform.position.x-camPosX) > camSize*16/9 || Mathf.Abs(transform.position.y - camPosY) > camSize)
    {
        tdpt.DisableProjectile();
    }
}

void OnTriggerEnter2D(Collider2D col) {
    if(col.gameObject.tag != "Player") {
        float directionX = Mathf.Sign(transform.localScale.x);
        float directionY = Mathf.Sign(col.transform.position.y- transform.position.y);
        //RaycastHit2D forward = Physics2D.Raycast(transform.position, transform.right * Mathf.Sign(transform.localScale.x), 0.5f + Mathf.Abs(transform.localScale.x)/2);
        RaycastHit2D up = Physics2D.Raycast(transform.position, transform.up, 0.5f + Mathf.Abs(transform.localScale.x)/2, LayerMask.GetMask("Ground"));
        RaycastHit2D down = Physics2D.Raycast(transform.position, -transform.up, 0.5f + Mathf.Abs(transform.localScale.x)/2, LayerMask.GetMask("Ground"));
        float positionX;
        if(down.collider != col && up.collider != col) // hitting vertical wall
        {
            positionX = col.transform.position.x - (col.transform.localScale.x/2*directionX)-tdpt.transform.localScale.x/2*directionX;
        } else // hitting horizontal floor/ceiling
        {
            positionX = transform.position.x;//+(transform.localScale.x/2)-(Mathf.Abs(tdpt.transform.localScale.x)*directionX/2);
        }               //  proj pos          - 1         +   
        float positionY = transform.position.y-up.distance+down.distance-(tdpt.transform.localScale.y/2*directionY);
        // positionX = Mathf.Round(positionX*16)/16;
        // positionY = Mathf.Round(positionY*16)/16;
        //print("pos: " + transform.position.y + " - " + up.distance + " + " + down.distance + " - " + tdpt.transform.localScale.y/2*directionY);
        //print("equals: " + positionY);
        print("pos: " + transform.position.x + " + " + transform.localScale.x/2 + "- " + Mathf.Abs(tdpt.transform.localScale.x)*directionX/2);
        print("equals: " + positionX);
        tdpt.Teleport(new Vector2(positionX, positionY));
    }
 }

}