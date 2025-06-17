using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject player;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] SpriteRenderer sRend;

    private Player pScript;

    private Vector3 pVec;

    private Rigidbody2D rb;

    private float enemySpeed = 2f;

    private float normalSpeed;

    private int frame = 0;

    private float damage = 20f;

    private bool isAttacking;

    private bool isCharging;

    private float stunTimer = 0f;

    private float stunTime = 1f;

    private bool canMove = true;

    private Vector2 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pScript = player.GetComponent<Player>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (frame % 5 == 0)
        {
            pVec = PathFind();
        }
        
        if (math.abs(pVec.y) > 0 && math.abs(pVec.x) > 0 && canMove)
        {
            normalSpeed = enemySpeed / math.sqrt(math.square(pVec.y) + math.square(pVec.x));
            rb.linearVelocity = new Vector3(pVec.x * normalSpeed, pVec.y * normalSpeed, 0);
        }
        else if (canMove)
        {
            rb.linearVelocity = new Vector3(pVec.x * enemySpeed, pVec.y * enemySpeed, 0);
        }

        Vector2 direction = (pVec).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), .25f);
        }

        isCharging = Physics2D.Raycast(transform.position, direction, 2.5f, playerLayer);

        isAttacking = Physics2D.Raycast(transform.position, direction, 0.75f, playerLayer);

        if (isCharging && canMove)
        {
            enemySpeed = 5f;
        }
        else
        {
            enemySpeed = 2f;
        }

        if (isAttacking && stunTimer <= 0)
        {
            pScript.Damage(damage);
            isAttacking = false;
            canMove = false;
            sRend.color = Color.yellow;
            stunTimer = stunTime;
        }

        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        else
        {
            sRend.color = Color.red;
            canMove = true;
        }

        if (pScript.pDead == true)
        {
            Reset();
        }
    }

    private Vector3 PathFind()
    {
        return (player.transform.position - transform.position);
    }

    private void Reset()
    {
        transform.position = startPosition;
        stunTimer = 0f;
        isAttacking = false;
        canMove = true;
    }
}
