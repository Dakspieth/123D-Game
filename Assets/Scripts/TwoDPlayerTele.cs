using UnityEngine;
using UnityEngine.InputSystem;


public class TwoDPlayerTele : MonoBehaviour
{
    public float projSpeed;
    public float shootWait;
        float baseShootWait;
    public InputAction shootControl;

    public GameObject proj; // projectile
    Rigidbody2D projRB;
    Rigidbody2D rb;

    void Start()
    {
        baseShootWait = shootWait;   
        projRB = proj.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        shootWait -= Time.deltaTime;

        if(shootControl.ReadValue<float>() == 1 && shootWait <= 0) {
            print("sus");
            proj.transform.position = transform.position;
            proj.SetActive(true);
            projRB.linearVelocity = new Vector2(projSpeed, rb.linearVelocity.y);
        }

        if(proj.activeSelf) {
            shootWait = baseShootWait;
        }

        if(shootWait <= 0) {
            proj.SetActive(false);
            projRB.linearVelocity = new Vector2(0, 0);
        }
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
