using UnityEngine;
using UnityEngine.InputSystem;


public class OneDPlayerMove : MonoBehaviour
{
    [SerializeField]
    public float playerSpeed;
    float direction;
    public InputAction controls;

public void OnEnable()
    {
        controls.Enable();
    }
 public void OnDisable()
    {
        controls.Disable();
    }
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = controls.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * playerSpeed, 0f);
    }
}
