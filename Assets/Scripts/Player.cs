using Unity.Mathematics;
using UnityEngine;

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

    private float normalSpeed;

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

        if (math.abs(dirY) > 0 && math.abs(dirX) > 0)
        {
            normalSpeed = playerSpeed / math.sqrt(math.square(dirY) + math.square(dirX));
            rb.linearVelocity = new Vector3(dirX * normalSpeed, dirY * normalSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(dirX * playerSpeed, dirY * playerSpeed, 0);
        }
    }
}
