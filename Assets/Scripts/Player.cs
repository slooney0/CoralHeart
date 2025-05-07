using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    //private fields

    /**
     * Float that represents the players X direction
     * Pressure/Stick sensitive (More you hold, higher the value)
     */
    private float dirX;

    /**
     * Float that represents the players Y direction
     * Pressure/Stick sensitive (More you hold, higher the value)
     */
    private float dirY;

    /** Float that represents the players move speed */
    private float playerSpeed = 7f;

    /** Float that represents the normalized move speed when moving at an angle */
    private float normalSpeed;

    /** Vector representing the location of the mouse */
    private Vector3 mouseInput;

    //Game object fields

    /** Represents the players rigidbody */
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (math.abs(dirY) > 0 && math.abs(dirX) > 0)
        {
            normalSpeed = playerSpeed / math.sqrt(math.square(dirY) + math.square(dirX));
            rb.linearVelocity = new Vector3(dirX * normalSpeed, dirY * normalSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(dirX * playerSpeed, dirY * playerSpeed, 0);
        }

        Vector2 direction = (mouseInput - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), .25f);

    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
