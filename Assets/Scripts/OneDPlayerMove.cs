using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OneDPlayerMove : MonoBehaviour
{
    [SerializeField]
    public float playerSpeed, projectileSpeed;

    float direction, lastDirection;
    float projectileTime = 1;

    [SerializeField]
    public float projectileMaxTime;

    public InputAction moveControl, shoot;

    [SerializeField]
    public GameObject projectilePrefab;

    GameObject projectile;
    Rigidbody2D rb, projRB;

    ResetScreen resetScreen;

public void OnEnable()
    {
        moveControl.Enable();
        shoot.Enable();
    }
 public void OnDisable()
    {
        moveControl.Disable();
        shoot.Disable();
    }
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        projectile = Instantiate(projectilePrefab);
        projRB = projectile.GetComponent<Rigidbody2D>();
        projectile.SetActive(false);
        resetScreen = GameObject.FindGameObjectWithTag("ResetScreen").GetComponent<ResetScreen>();
        resetScreen.AddToList(gameObject, transform.position);
    }

    void Update()
    {
        direction = moveControl.ReadValue<float>();
        lastDirection = direction!=0 ? direction : lastDirection; // set to the direction the character last moved
        if (shoot.WasPressedThisFrame() && !projectile.activeSelf)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * playerSpeed, 0f);
        if(projectileTime > projectileMaxTime)
        {
            projRB.linearVelocity = Vector2.zero;
            projectile.SetActive(false);
        } else
        {
            projectileTime += Time.deltaTime; 
        }
    }

    void Shoot()
    {   
        projectileTime = 0;
        projectile.SetActive(true);
        // makes sure the position is set to the edge of the player to make the proj shoot instant
        projectile.transform.position = (Vector2) transform.position/* + new Vector2(lastDirection*((projectile.transform.localScale.x + gameObject.transform.localScale.x) / 2),0)*/;
        projRB.linearVelocityX = lastDirection * projectileSpeed +rb.linearVelocityX*0.25f;
        
    }

    public void Teleport(float position, float dir, float colScale)
    {
        
        float telePosX = position + ((gameObject.transform.localScale.x + colScale) / 2 * dir);
        gameObject.transform.localPosition = new Vector2(telePosX, 0);

        projectileTime = projectileMaxTime;
        projectile.SetActive(false);   

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "BorderRight")
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize*16/9*2, 0, -10);
            StartCoroutine(SwitchScreen(Camera.main.gameObject, Camera.main.transform.position, new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize*16/9*2, 0, -10)));
            //gameObject.transform.position = new Vector2(transform.position.x + 1.5f, 0);
            StartCoroutine(SwitchScreen(gameObject, transform.position, new Vector3(transform.position.x + 1.5f, 0, 0)));

        } else if (col.gameObject.tag == "BorderLeft")
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize*16/9*2, 0, -10);
            StartCoroutine(SwitchScreen(Camera.main.gameObject, Camera.main.transform.position, new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize*16/9*2, 0, -10)));
            //gameObject.transform.position = new Vector2(transform.position.x - 1.5f, 0);
            StartCoroutine(SwitchScreen(gameObject, transform.position, new Vector3(transform.position.x - 1.5f, 0, 0)));
        }
    }

    IEnumerator SwitchScreen(GameObject obj, Vector3 start, Vector3 end)
    {
        float duration = 0.5f;
        float timeElapsed = 0f;
        Teleport(gameObject.transform.position.x, 1, -gameObject.transform.localScale.x);
        while (timeElapsed < duration)
        {
            float t = timeElapsed/duration;
            obj.transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        resetScreen.AddToList(gameObject, transform.position);
        obj.transform.position = end;
    }
}
