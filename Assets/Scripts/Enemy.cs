using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject player;

    private Vector3 pVec;

    private Rigidbody2D rb;

    private float enemySpeed = 2f;

    private float normalSpeed;

    private int frame = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (frame % 5 == 0)
        {
            pVec = PathFind();
        }
        
        if (math.abs(pVec.y) > 0 && math.abs(pVec.x) > 0)
        {
            normalSpeed = enemySpeed / math.sqrt(math.square(pVec.y) + math.square(pVec.x));
            rb.linearVelocity = new Vector3(pVec.x * normalSpeed, pVec.y * normalSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(pVec.x * enemySpeed, pVec.y * enemySpeed, 0);
        }

        Vector2 direction = (pVec).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), .25f);
    }

    private Vector3 PathFind()
    {
        return (player.transform.position - transform.position);
    }
}
