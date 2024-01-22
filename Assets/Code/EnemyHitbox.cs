using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            // Create a new damage object, before sending it the player
            Damage dmg = new Damage()
            {
                damageAmount = damage,
                origin = transform.position,
                pushforce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
