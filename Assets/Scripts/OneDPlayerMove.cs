using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OneDPlayerMove : MonoBehaviour
{
    [SerializeField]
    public float playerSpeed, projectileSpeed;

    float direction;
    float lastDirection = 1;
    float projectileTime = 1;

    [SerializeField]
    public float projectileMaxTime;

    public InputAction moveControl, shoot;

    [SerializeField]
    public GameObject projectilePrefab;

    [HideInInspector]
    public bool haveShootItem;
    GameObject projectile;
    Rigidbody2D rb, projRB;

    ResetScreen resetScreen;
    bool transitioning = false; // true when shifting screens, used to prevent shooting and moving-based bugs

    [SerializeField]
    public List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;

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
        
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direction = !transitioning ? moveControl.ReadValue<float>() : 0;
        
        lastDirection = direction!=0 ? direction : lastDirection; // set to the direction the character last moved

        sr.flipX = lastDirection == 1 ? false : true; 
        if (shoot.WasPressedThisFrame() && !projectile.activeSelf && !transitioning && haveShootItem)
        {
            Shoot();
        }
        if (haveShootItem)
        {
            sr.sprite = projectile.activeSelf ? sprites[1] : sprites[0];  
        } else
        {
            sr.sprite = sprites[2];
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * playerSpeed, 0f);
        if(projectileTime > projectileMaxTime || transitioning)
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
        gameObject.transform.localPosition = new Vector2(telePosX, transform.position.y);

        projectileTime = projectileMaxTime;
        projectile.SetActive(false);   

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "BorderRight")
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize*16/9*2, 0, -10);
            StartCoroutine(SwitchScreen(Camera.main.gameObject, Camera.main.transform.position, new Vector3(Camera.main.transform.position.x + Camera.main.orthographicSize*16/9*2, Camera.main.transform.position.y, -10)));
            //gameObject.transform.position = new Vector2(transform.position.x + 1.5f, 0);
            StartCoroutine(SwitchScreen(gameObject, transform.position, new Vector3(transform.position.x + 1.5f, transform.position.y, 0)));

        } else if (col.gameObject.tag == "BorderLeft")
        {
            //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize*16/9*2, 0, -10);
            StartCoroutine(SwitchScreen(Camera.main.gameObject, Camera.main.transform.position, new Vector3(Camera.main.transform.position.x - Camera.main.orthographicSize*16/9*2, Camera.main.transform.position.y, -10)));
            //gameObject.transform.position = new Vector2(transform.position.x - 1.5f, 0);
            StartCoroutine(SwitchScreen(gameObject, transform.position, new Vector3(transform.position.x - 1.5f, transform.position.y, 0)));
        }
    }

    IEnumerator SwitchScreen(GameObject obj, Vector3 start, Vector3 end)
    {
        transitioning = true;

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
        transitioning = false;
        resetScreen.AddToList(gameObject, transform.position);
        obj.transform.position = end;
    }
}
