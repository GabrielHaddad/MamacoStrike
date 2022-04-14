using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class IHittable : MonoBehaviour
{
    public float HP
    {
        get
        {
            Init();

            return m_hp;
        }

        set
        {
            Init();

            float v = Mathf.Clamp(value,0,m_maxhp);

            if(m_hp != v)
            {
                if(HPBar && Bar)
                {
                    if(DOTween.IsTweening(Bar)) DOTween.Kill(Bar);

                    Bar.alpha = 1;
                    Bar.DOFade(0,1).SetDelay(2).SetEase(Database.SmoothCurve);
                }
            }
            
            m_hp = v;

            if(m_hp == 0)
            {
                OnDeath();
            }

            UpdateVisual();
        }
    }

    public float MaxHP
    {
        get
        {
            Init();

            return m_maxhp;
        }

        set
        {
            Init();

            m_maxhp = Mathf.Max(value,0);

            HP = Mathf.Min(m_hp,m_maxhp);
        }
    }

    public float StartingMaxHP = 1;
    public float DelayToDie = 0;
    public Color DamageColor = Color.white;
    public bool HPBar = true;

    [HideInInspector]
    public UnityEvent OnDamageTaken = new UnityEvent();

    private float m_hp = 1;
    private float m_maxhp = 1;

    private CanvasGroup Bar = null;
    private Transform Fill = null;

    private bool initted = false;

    public void DealDamage(float Value)
    {
        UIUtils.Instance.DamageText(Value,transform,DamageColor);
        HP -= Value;
        
        OnDamageTaken?.Invoke();
    }

    protected virtual void OnDeath()
    {
        if(DelayToDie > 0)
        {
            Destroy(gameObject,DelayToDie);   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateVisual()
    {
        if(HPBar)
        {
            if(Fill)
            {
                if(m_maxhp == 0)
                {
                    Fill.localScale = new Vector3(0,1,1);
                }
                else
                {
                    Fill.localScale = new Vector3(m_hp/m_maxhp,1,1);
                }
            }
        }
    }

    private void Init()
    {
        if(initted) return;
        initted = true;

        m_hp = StartingMaxHP;
        m_maxhp = StartingMaxHP;

        if(HPBar)
        {
            GameObject go = GameObject.Instantiate(Database.Prefabs[1]);
            go.GetComponent<RectTransform>().SetParent(transform,false);

            Fill = go.transform.GetChild(0).GetChild(0).GetChild(0).transform;
            Bar = go.GetComponent<CanvasGroup>();
            Bar.alpha = 0;

            UpdateVisual();
        }
    }
}
