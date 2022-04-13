using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatAnimationTransform : MonoBehaviour
{
    public float Distance = 10;
    public float Interval = 1;

    bool started = false;

    Transform transf = null;

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
        Transform rect = GetComponent<Transform>();
        if(rect != null)
        {
            transf = rect;
            
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

            if(transf == null)
            {
                transf = GetComponent<Transform>();

                if(transf == null) yield break;
            }

            transf.position = transf.position + Vector3.down * pos;
            pos = 0;


            while(accumulatedTime < storedInterval)
            {
                if(transf != null)
                {
                    accumulatedTime += Time.deltaTime;

                    float currentMovement = (Mathf.Sin(doublePi*accumulatedTime/storedInterval) * Distance) - pos;

                    transf.position = transf.position + Vector3.up * currentMovement;

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
