using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BaseWeapon : MeleeWeapon
{
    public BaseWeapon(GameObject player) : base("base", 10f, 0.5f, player) { }

    public override void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(new Vector2(player.transform.position.x + (Mathf.Cos(player.transform.eulerAngles.z * Mathf.PI / 180)) * 1.1f, player.transform.position.y + (Mathf.Sin(player.transform.eulerAngles.z * Mathf.PI / 180)) * 1.1f), new Vector2(1f, 1f), 0f);
        foreach (Collider2D col in hit)
        {
            Debug.Log("Yes");
            Enemy eScript = col.GetComponent<Enemy>();
            if (eScript != null)
            {
                eScript.Damage(10f);
                Debug.Log("Hit you!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector2(player.transform.position.x + Mathf.Cos(player.transform.eulerAngles.z * Mathf.PI / 180), player.transform.position.y + Mathf.Sin(player.transform.eulerAngles.z * Mathf.PI / 180)), new Vector2(1f, 1f));
    }
}
