using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDPlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public InputAction moveControl;
    public float jumpForce;
    public InputAction jumpControl;
    
    Rigidbody2D rb;
    float moveDir;
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveControl.Enable();
        jumpControl.Enable();
    }
    void OnDisable()
    {
        moveControl.Disable();
        jumpControl.Disable();
    }
    void FixedUpdate()
    {
        moveDir = moveControl.ReadValue<float>();
        print(grounded);
        
        grounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), new Vector2(transform.localScale.x-0.01f, 0.1f), 0f, LayerMask.GetMask("Ground"));
        print(grounded);
        float velX = rb.linearVelocity.x;
        velX = Vector2.ClampMagnitude(velX, moveSpeed);
        rb.linearVelocity = new Vector2(velX, rb.linearVelocity.y);
        rb.linearVelocity += new Vector2(moveDir * moveSpeed * Time.fixedDeltaTime, 0);
    }
}
