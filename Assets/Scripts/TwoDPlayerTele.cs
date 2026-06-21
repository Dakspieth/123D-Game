using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class TwoDPlayerTele : MonoBehaviour
{
    public float projSpeed;
    public float shootWait;
        float baseShootWait;
    public InputAction shootControl, upDownControl;

    public GameObject proj; // projectile
    [HideInInspector]
    public Rigidbody2D projRB;
    Rigidbody2D rb;
    TwoDPlayerMove tdpm;
    
    void Start()
    {
        baseShootWait = shootWait;   
        shootWait = 0;
        projRB = proj.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        tdpm = GetComponent<TwoDPlayerMove>();
    }


    void Update()
    {
        shootWait -= shootWait > -1 ? Time.deltaTime : 0;
        
        if(shootControl.ReadValue<float>() == 1 && shootWait <= 0) {
            proj.SetActive(true);
            proj.transform.position = transform.position;
            proj.transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x) * Mathf.Abs(proj.transform.localScale.x), proj.transform.localScale.y);
            float move = tdpm.moveControl.ReadValue<float>();
            float up = upDownControl.ReadValue<float>();
            float dir = transform.localScale.x;
            // x = move || !up
            // y = move || up
            projRB.linearVelocity = new Vector2(projSpeed * dir * Mathf.Max(Mathf.Abs(move), Mathf.Abs(Mathf.Abs(up)-1)), projSpeed * up * Mathf.Max(Mathf.Abs(up), Mathf.Abs(move)));

        }

        if(proj.activeSelf) {
            shootWait = baseShootWait;
        }
        
    }

    public void Teleport(Vector2 telePosition)
    {
        print(telePosition);
        transform.position = telePosition;
        tdpm.coyoteTime = -1;
        DisableProjectile();
    }

    public void DisableProjectile() {
        proj.SetActive(false);
        projRB.linearVelocity = new Vector2(0, 0);
    }

    void OnEnable()
    {
        shootControl.Enable();
        upDownControl.Enable();
    }
    void OnDisable()
    {
        shootControl.Disable();
        upDownControl.Disable();
    }
}
