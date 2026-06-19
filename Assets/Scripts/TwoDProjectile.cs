using UnityEngine;

public class TwoDProjectile : MonoBehaviour
{
TwoDPlayerTele tdpt;

void Start() {
    tdpt = GameObject.FindGameObjectWithTag("Player").GetComponent<TwoDPlayerTele>();
    gameObject.SetActive(false);
}

void Update() {
    if(Mathf.Abs(transform.position.x-Camera.main.transform.position.x) > Camera.main.orthographicSize*2)
    {
        tdpt.DisableProjectile();
    }
}

void OnTriggerEnter2D(Collider2D col) {
    if(col.gameObject.tag != "Player") {
        float directionX = Mathf.Sign(transform.localScale.x);
        float directionY = Mathf.Sign(col.transform.position.y- transform.position.y);
        //RaycastHit2D forward = Physics2D.Raycast(transform.position, transform.right * Mathf.Sign(transform.localScale.x), 0.5f + Mathf.Abs(transform.localScale.x)/2);
        RaycastHit2D up = Physics2D.Raycast(transform.position, transform.up, 0.5f + Mathf.Abs(transform.localScale.x)/2);
        RaycastHit2D down = Physics2D.Raycast(transform.position, -transform.up, 0.5f + Mathf.Abs(transform.localScale.x)/2);
        
        float positionX = transform.position.x+(transform.localScale.x/2)-(Mathf.Abs(tdpt.transform.localScale.x*directionX)/2);
        float positionY = transform.position.y-up.distance+down.distance-(transform.localScale.y/2*directionY);

        tdpt.Teleport(new Vector2(positionX, positionY));

        tdpt.DisableProjectile();
    }
 }

}