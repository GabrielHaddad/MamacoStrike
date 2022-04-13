using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatAnimation : MonoBehaviour
{
    public float Distance = 10;
    public float Interval = 1;

    bool started = false;

    RectTransform m_rect = null;

    public void Set(float dist, float intv)
    {
        Distance = dist;
        Interval = intv;
    }

    public void Init()
    {
        if(!started) Start();
    }

    private void Start()
    {
        started = true;
        RectTransform rect = GetComponent<RectTransform>();
        if(rect != null)
        {
            m_rect = rect;
            
            StartCoroutine(Run());
        }
        else
        {
            print("Failed to find target to start coroutine!");
        }
    }

    private IEnumerator Run()
    {
        float doublePi = Mathf.PI*2;
        float pos = 0;

        while(true)
        {
            if(Interval <= 0)
            {
                yield return new WaitUntil(() => Interval > 0);
            } 

            float accumulatedTime = 0;
            float storedInterval = Interval;

            if(m_rect == null)
            {
                m_rect = GetComponent<RectTransform>();

                if(m_rect == null) yield break;
            }

            m_rect.anchoredPosition = m_rect.anchoredPosition + Vector2.down * pos;
            pos = 0;


            while(accumulatedTime < storedInterval)
            {
                if(m_rect != null)
                {
                    accumulatedTime += Time.deltaTime;

                    float currentMovement = (Mathf.Sin(doublePi*accumulatedTime/storedInterval) * Distance) - pos;

                    m_rect.anchoredPosition = m_rect.anchoredPosition + Vector2.up * currentMovement;

                    pos += currentMovement;
                
                    yield return null;
                }
                else
                {
                    yield break;
                }
            }
        }   
    }
}
