using UnityEngine;

public class MultiDWall : MonoBehaviour
{
    [SerializeField]
    public float offset, timeOn, timeOff;
    bool on = true;
    float timer = 0;
    SpriteRenderer sr;
    BoxCollider2D bc;

    GameObject player;

    ResetScreen resetScreen;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();   
        player = GameObject.FindGameObjectWithTag("Player"); 
        resetScreen = GameObject.FindGameObjectWithTag("ResetScreen").GetComponent<ResetScreen>();
        timer = offset;
    }

    void Update()
    {
        sr.enabled = on;
        bc.enabled = on;
        if(on && timer < timeOn)
        {
            timer += Time.deltaTime;
        } else if (!on && timer < timeOff)
        {
            timer += Time.deltaTime;
        } else
        {
            on = !on;
            timer = 0;
            if(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < player.transform.localScale.x/2)
            {
                resetScreen.ReloadScreen();
                print("die");
            }
        }
    }
}
