using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LayerCollider : MonoBehaviour
{
    public int TargetLayer = 6;

    [HideInInspector]
    public List<GameObject> BlackList = new List<GameObject>();

    [HideInInspector]
    public Action<Collider2D> OnEnter = null;
    [HideInInspector]
    public Action<Collider2D> OnStay = null;
    [HideInInspector]
    public Action<Collider2D> OnExit = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == TargetLayer && !BlackList.Contains(col.gameObject))
        {
            if(OnEnter != null) OnEnter(col);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == TargetLayer && !BlackList.Contains(col.gameObject))
        {
            if(OnStay != null) OnStay(col);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == TargetLayer && !BlackList.Contains(col.gameObject))
        {
            if(OnExit != null) OnExit(col);
        }
    }
}
