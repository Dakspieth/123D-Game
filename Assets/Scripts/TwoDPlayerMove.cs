using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDPlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public InputAction moveControl;
    public float jumpForce;
    public float jumpTime; // how long to stay in air while holding jump
        float baseJumpTime;
        bool readJump = true;
    public InputAction jumpControl;
    
    Rigidbody2D rb;
    float moveDir;
    bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseJumpTime = jumpTime;
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
        grounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), // arg 1
                                        new Vector2(transform.localScale.x-0.01f, 0.1f), 0f, LayerMask.GetMask("Ground"));    // arg 2

        moveDir = moveControl.ReadValue<float>(); // left right
        if(grounded && jumpControl.ReadValue<float>() == 1 && readJump) {
            jumpTime = baseJumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            readJump = false;
        } else if(!grounded && jumpControl.ReadValue<float>() == 1 && jumpTime > 0) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        } else if(jumpControl.ReadValue<float>() == 0) {
            readJump = true;
        }

        jumpTime -= Time.fixedDeltaTime;

        
        Vector2 moveSpeedDir = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = moveSpeedDir;
    }
    
}
