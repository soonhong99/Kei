using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private CapsuleCollider2D capsuleCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        capsuleCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            // Debug.Log(hits[i].name);

            OnCollide(hits[i]);

            // The array is not cleaned up, so we do it ourself
            hits[i] = null;
        }
        
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not implemented in " + this.name);
    }
}
