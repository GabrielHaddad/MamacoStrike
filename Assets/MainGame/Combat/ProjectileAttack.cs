using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : AreaEffect
{
    public float Damage = 1;

    protected override void Init()
    {
        ShouldHitWall = true;
        Velocity = 15f;
        Duration = 4f;
    } 

    public override void OnHitEnter(Collider2D col)
    {
        var hittable = col.gameObject.GetComponentInChildren<IHittable>();

        if(hittable) hittable.DealDamage(Damage);

        DestroySelf();
    }

    public override void OnWallEnter(Collider2D col)
    {
        DestroySelf();
    }
}
