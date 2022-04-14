using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AreaEffect
{
    public float Damage = 1;

    public Action<Collider2D> OnHitCallback = null;

    private List<IHittable> hits = new List<IHittable>();

    protected override void Init()
    {
        ShouldHitWall = false;
        Velocity = 0.7f;
        Duration = 0.15f;
    } 

    public override void OnHitEnter(Collider2D col)
    {
        var hittable = col.gameObject.GetComponentInChildren<IHittable>();

        if(hittable)
        {
            if(!hits.Contains(hittable))
            {
                hits.Add(hittable);
                hittable.DealDamage(Damage);
                if(OnHitCallback != null) OnHitCallback(col);
            }
        }
    }
}
