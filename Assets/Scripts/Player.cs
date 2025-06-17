using System.Collections;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
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

    /** Float that represents the power of the player's dash */
    private float dashingPower = 25f;

    /** Float that represents how long the player must wait before they can dash again */
    private float dashCoolDown = 1.5f;

    /** Float that represents the timer for the dash cool down period */
    private float dashCoolDownCounter = 0.75f;

    /** FLoat that represents the amount of time the player is dashing for */
    private float dashingTime = 0.175f;

    /** Float that represents the normalized move speed when moving at an angle */
    private float normalSpeed;

    /** Boolean that controls whether or not the player can move */
    private bool canMove = true;

    /** Vector representing the location of the mouse */
    private Vector3 mouseInput;

    /** Float that represents the angle of the player in radians */
    float angleRad;

    private float startHealth = 100f;

    private float health;

    private Vector3 startingPosition = new Vector3(0,0,0);

    public bool pDead = false;

    //Game object fields

    /** Represents the players rigidbody */
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCoolDownCounter = dashCoolDown;
        health = startHealth;
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (math.abs(dirY) > 0 && math.abs(dirX) > 0 && canMove)
        {
            normalSpeed = playerSpeed / math.sqrt(math.square(dirY) + math.square(dirX));
            rb.linearVelocity = new Vector3(dirX * normalSpeed, dirY * normalSpeed, 0);
        }
        else if (canMove)
        {
            rb.linearVelocity = new Vector3(dirX * playerSpeed, dirY * playerSpeed, 0);
        }

        Vector2 direction = (mouseInput - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), .25f);

        angleRad = angle * (Mathf.PI / 180);

        if (Input.GetKey(KeyCode.LeftShift) && dashCoolDownCounter <= 0)
        {
            StartCoroutine(Dash());
            dashCoolDownCounter = dashCoolDown;
        }
        if (dashCoolDownCounter > 0)
        {
            dashCoolDownCounter -= Time.deltaTime;
        }


        //To Be Removed
        if (pDead)
        {
            pDead = false;
        }
    }

    public IEnumerator Dash()
    {
        canMove = false;

        Vector2 vel = rb.linearVelocity;

        rb.linearVelocity += (new Vector2((Mathf.Cos(angleRad) * dashingPower), (Mathf.Sin(angleRad) * dashingPower)));

        yield return new WaitForSeconds(dashingTime);

        rb.linearVelocity = vel;
        canMove = true;
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log("Damaged: " + health);
        if (health <= 0)
        {
            playerDeath();
        }
    }

    private void playerDeath()
    {
        pDead = true;
        health = startHealth;
        transform.position = startingPosition;
    }
}
