using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //Collider2D attackCollier2D;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    //private void Awake()
    //{
    //    attackCollier2D = GetComponent<Collider2D>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hitTrigger
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // if it can be hitTrigger, hitTrigger it
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log("Hit: " + collision.name + "for" + attackDamage);
            }
        }
    }
}
