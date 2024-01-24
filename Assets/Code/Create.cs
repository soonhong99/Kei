using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create 가 아니라 crate(나무상자)
public class Create : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
