using UnityEngine;
using UnityEngine.InputSystem;


public class TwoDPlayerTele : MonoBehaviour
{
    public float projSpeed;
    public float shootWait;
        float baseShootWait;
    public InputAction shootControl;

    public GameObject proj; // projectile
    [HideInInspector]
    public Rigidbody2D projRB;
    Rigidbody2D rb;
    TwoDPlayerMove tdpm;
    void Start()
    {
        baseShootWait = shootWait;   
        projRB = proj.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        tdpm = GetComponent<TwoDPlayerMove>();
    }


    void Update()
    {
        shootWait -= shootWait > -1 ? Time.deltaTime : 0;

        if(shootControl.ReadValue<float>() == 1 && shootWait <= 0) {
            proj.transform.position = transform.position;
            proj.SetActive(true);
            proj.transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x) * Mathf.Abs(proj.transform.localScale.x), proj.transform.localScale.y);
            projRB.linearVelocity = new Vector2(projSpeed * Mathf.Sign(transform.localScale.x), rb.linearVelocityY *0.5f);
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
    }

    public void DisableProjectile() {
        proj.SetActive(false);
        projRB.linearVelocity = new Vector2(0, 0);
    }

    void OnEnable()
    {
        shootControl.Enable();
    }
    void OnDisable()
    {
        shootControl.Disable();
    }
}
