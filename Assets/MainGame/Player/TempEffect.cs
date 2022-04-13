using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEffect
{
    public float TotalDuration = 1;



    private float CurrentDuration = 1;

    private PlayerController m_controller = null;

    public void Init(PlayerController m_controller)
    {
        CurrentDuration = TotalDuration;

        this.m_controller = m_controller;

        OnAdd();
    }

    protected virtual void OnAdd() { }

    protected virtual void OnExpire() { } 

    public void Tick()
    {
        CurrentDuration -= Time.deltaTime;
        
        bool willExpire = CurrentDuration <= 0;

        EffectTick(willExpire);

        if(willExpire) {

            OnExpire();

            m_controller.CurrentEffects.Remove(this);
        }
    }

    protected virtual void EffectTick(bool willExpire) { }
}
