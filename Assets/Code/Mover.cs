using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;

    public Transform weapon;

    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    protected CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid_any;
    Animator anim;
    SpriteRenderer spriter_any;

    // 맨처음에 한번 호출되는 함수
    private void Awake()
    {
        spriter_any = GetComponent<SpriteRenderer>();
        rigid_any = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        originalSize = transform.localScale;
    }
}
