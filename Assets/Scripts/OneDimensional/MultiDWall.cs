using System.Collections.Generic;
using UnityEngine;

public class MultiDWall : MonoBehaviour
{
    [Header("Time-Based")]
    [SerializeField]
    public bool timeBased = true;
    public float offset, timeOn, timeOff;
    public bool on = true;
    
    [Header("State-Based")]
    public List<bool> states = new List<bool>();
    public float stateTime;
    int currState = 0;

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
        timer = -offset;
        currState = states.Count-1;
    }

    void Update()
    {
        if (timeBased)
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
        } else {
            if(timer > stateTime)
            {
                    // checks if the next state is different than the current
                if(states [currState] != states[currState+1<states.Count ? currState+1 : 0] && Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < player.transform.localScale.x/2)
                {
                    resetScreen.ReloadScreen();
                    print("die");
                }
                currState += currState+1 < states.Count ? 1 : -currState; // increments currstate if in range, else set to 0
                timer = 0;
                sr.enabled = states[currState];
                bc.enabled = states[currState];
                
            } else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
