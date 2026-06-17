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
    public float coyoteTime;
        float baseCoyoteTime;
    public InputAction jumpControl;
    
    Rigidbody2D rb;
    float moveDir;
    bool groundCollided;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseJumpTime = jumpTime;
        baseCoyoteTime = coyoteTime;
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
        groundCollided = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2),
                                              new Vector2(transform.localScale.x-0.02f, 0.1f), 0f, LayerMask.GetMask("Ground"));
        if(groundCollided) {
            coyoteTime = baseCoyoteTime;
        } else {
            coyoteTime -= Time.deltaTime;
        }

        moveDir = moveControl.ReadValue<float>(); // left right

        

            // instead of grounded bool
        if(coyoteTime>0 && jumpControl.ReadValue<float>() == 1 && readJump) {
            jumpTime = baseJumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //readJump = false;
        } else if(!groundCollided && jumpControl.ReadValue<float>() == 1 && jumpTime > 0) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
         }// else if(jumpControl.ReadValue<float>() == 0) {
        //     readJump = true;
        // }
        if(jumpControl.ReadValue<float>() == 1) {
            readJump = false;
        } else {
            readJump = true;
        }

        jumpTime -= Time.fixedDeltaTime;

        
        Vector2 moveSpeedDir = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = moveSpeedDir;
    }
    
}
