using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create �� �ƴ϶� crate(��������)
public class Create : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
