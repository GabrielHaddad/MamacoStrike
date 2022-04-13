using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float Velocity = 0;

    public int TargetHit = 6;

    public bool ShouldHitWall = false;

    public float Duration = 2;

    [HideInInspector]
    public List<GameObject> BlackList = new List<GameObject>();

    private LayerCollider hit;
    private LayerCollider wall;

    private void Start()
    {
        Init();

        StartCoroutine(ExpireCoroutine());

        Transform hitT = transform.Find("HitTrigger");

        if(!hitT)
        {
            hitT = (new GameObject("HitTrigger")).transform;
            hitT.SetParent(transform);
            hitT.localPosition = Vector3.zero;
            hitT.localScale = Vector3.one*1.1f;
            hitT.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }


        hit = hitT.GetComponent<LayerCollider>();

        if(!hit) hit = hitT.gameObject.AddComponent<LayerCollider>();

        hit.TargetLayer = TargetHit;
        hit.BlackList = BlackList;
        hit.OnEnter = OnHitEnter;
        hit.OnStay = OnHitStay;
        hit.OnExit = OnHitExit;


        if(ShouldHitWall)
        {
            Transform wallT = transform.Find("WallTrigger");

            if(!wallT)
            {
                wallT = (new GameObject("WallTrigger")).transform;
                wallT.SetParent(transform);
                wallT.localPosition = Vector3.zero;
                wallT.localScale = Vector3.one*0.8f;
                wallT.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            }

            wall = wallT.GetComponent<LayerCollider>();

            if(!wall) wall = wallT.gameObject.AddComponent<LayerCollider>();

            wall.TargetLayer = LayerMask.NameToLayer("Wall");
            wall.OnEnter = OnWallEnter;
            wall.OnStay = OnWallStay;
            wall.OnExit = OnWallExit;
        }
    }

    private void Update()
    {
        if(Velocity != 0)
        {
            transform.position += (Vector3) (Velocity * Time.deltaTime * transform.right);
        }

        Tick();
    }

    protected virtual void Init() { }

    protected virtual void Tick() { }

    protected virtual void OnExpire() { }

    public virtual void OnHitEnter(Collider2D col) { }
    public virtual void OnHitStay(Collider2D col) { }
    public virtual void OnHitExit(Collider2D col) { }
    public virtual void OnWallEnter(Collider2D col) { }
    public virtual void OnWallStay(Collider2D col) { }
    public virtual void OnWallExit(Collider2D col) { }

    protected void DestroySelf()
    {
        OnExpire();
        Destroy(gameObject);
    }

    private IEnumerator ExpireCoroutine()
    {
        yield return new WaitForSeconds(Duration);

        DestroySelf();
    }
}
