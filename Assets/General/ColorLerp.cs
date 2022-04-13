using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    public bool ShouldLerp = true;
    public Color TargetColor = Color.white;
    public float Interval = 1;

    bool started = false;
    Graphic m_graphic = null;
    private Color startColor;

    public void Set(Color target, float intv)
    {
        TargetColor = target;
        Interval = intv;
    }

    public void Init()
    {
        if(!started) Start();
    }

    private void Start()
    {
        started = true;
        Graphic g = GetComponent<Graphic>();
        if(g != null)
        {
            m_graphic = g;
            startColor = m_graphic.color;

            StartCoroutine(Run());
        }
        else
        {
            print("Failed to find target to apply color!");
        }
    }

    private void ResetColor()
    {
        if(m_graphic) m_graphic.color = startColor;
    }

    private IEnumerator Run()
    {

        while(true)
        {
            ResetColor();
            if(!ShouldLerp || Interval <= 0)
            {
                yield return new WaitUntil(() => ShouldLerp && Interval > 0);
            } 

            float halfInterval = Interval/2;

            m_graphic?.DOColor(TargetColor,halfInterval);
            yield return new WaitForSeconds(halfInterval);
            m_graphic?.DOColor(startColor,halfInterval);
            yield return new WaitForSeconds(halfInterval);
        }   
    }
}
