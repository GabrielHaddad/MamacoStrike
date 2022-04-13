using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public float Value
    {
        get
        {
            return m_value;
        }
    }

    public float BaseValue
    {
        get
        {
            return m_baseValue;
        }

        set
        {
            m_baseValue = value;
            CalculateValue();
        }
    }

    public List<float> FlatAdds {
        get
        {
            return m_flatadds;
        }

        set
        {
            m_flatadds = value;
            CalculateValue();
        }
    }

    public List<float> Multipliers {
        get
        {
            return m_mults;
        }

        set
        {
            m_mults = value;
            CalculateValue();
        }
    }

    private float m_value = 0;
    private float m_baseValue = 0;
    private List<float> m_flatadds = new List<float>();
    private List<float> m_mults = new List<float>();

    public float CalcWithAdd(float Add)
    {
        float adds = 0;
        foreach(float f in m_flatadds)
        {
            adds += f;
        }

        float mult = 1;
        foreach(float f in m_mults)
        {
            mult *= 1 + f;
        }

        return (m_baseValue + adds + Add) * mult;
    }

    public float CalcWithMult(float extraMult)
    {
        float adds = 0;
        foreach(float f in m_flatadds)
        {
            adds += f;
        }

        float mult = 1;
        foreach(float f in m_mults)
        {
            mult *= 1 + f;
        }

        return (m_baseValue + adds) * mult * (1 + extraMult);
    }

    private void CalculateValue()
    {
        float adds = 0;
        foreach(float f in m_flatadds)
        {
            adds += f;
        }

        float mult = 1;
        foreach(float f in m_mults)
        {
            mult *= 1 + f;
        }

        m_value = (m_baseValue + adds) * mult;
    }
}
