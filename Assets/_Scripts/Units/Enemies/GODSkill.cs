using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODSkill : MonoBehaviour
{
    public int damage = 20;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // if it can be hitTrigger, hitTrigger it
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log("Hit: " + collision.name + " for " + damage);

                //Destroy(gameObject);
            }
        }
    }
}
