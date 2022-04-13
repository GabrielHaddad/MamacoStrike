using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUtils : MonoBehaviour
{
    public static UIUtils Instance {
        get
        {
            if(m_instance == null)
            {
                Canvas canv = FindObjectOfType<Canvas>();
                m_instance = canv.gameObject.AddComponent<UIUtils>();
                m_instance.m_canvas = canv;
                m_instance.m_camera = Camera.main;
            }

            return m_instance;
        }
    }

    private static UIUtils m_instance = null;

    private Camera m_camera;
    private Canvas m_canvas;

    public void DamageText(float Value, Transform Tr, Color? c = null)
    {
        StartCoroutine(DamageTextCoroutine(Value,Tr,c ?? Color.white));
    }

    private IEnumerator DamageTextCoroutine(float Value, Transform Tr, Color c)
    {
        float floatDuration = 1;
        Value = Mathf.Round(Value * 100)/100;

        GameObject go = Instantiate(Database.Prefabs[0]);
        RectTransform rect = go.GetComponent<RectTransform>();
        Text[] txs = go.GetComponentsInChildren<Text>();

        rect.SetParent(m_canvas.transform,false);
        rect.position = m_camera.WorldToScreenPoint(Tr.position);

        rect.anchoredPosition = rect.anchoredPosition - rect.GetChild(0).GetComponent<RectTransform>().anchoredPosition + Vector2.up*15;

        foreach(Text tx in txs)
        {
            tx.text = Value.ToString();

            if(tx.gameObject != go)
            {
                tx.color = c;
            }
        }
        
        rect.DOAnchorPos(rect.anchoredPosition + Vector2.up*10,floatDuration - 0.2f);

        yield return new WaitForSeconds(floatDuration - 0.2f);
        foreach(Text tx in txs) tx.SetAlpha(0.5f);
        yield return new WaitForSeconds(0.05f);
        foreach(Text tx in txs) tx.SetAlpha(1f);
        yield return new WaitForSeconds(0.05f);
        foreach(Text tx in txs) tx.SetAlpha(0.5f);
        yield return new WaitForSeconds(0.05f);
        foreach(Text tx in txs) tx.SetAlpha(1f);
        yield return new WaitForSeconds(0.05f);

        Destroy(go);
    }

    private void OnDestroy()
    {
        m_instance = null;
    }
}
