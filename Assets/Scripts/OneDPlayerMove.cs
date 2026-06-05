using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
        gameObject.transform.localPosition = new Vector2(position + ((gameObject.transform.localScale.x + colScale) / 2 * dir), 0);
        projectileTime = projectileMaxTime;
        projectile.SetActive(false);
    }
}
