using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public float Radius = 1;
    public float Frequency = 2.5f;

    private Vector3 offset;

    public void ResetPos()
    {
        transform.position -= offset;
        offset = Vector3.zero;
    }

    private void Start()
    {
        StartCoroutine(Updating());
    }

    private IEnumerator Updating()
    {
        float previousAngle = 0;

        while(true)
        {
            float newAngle = Mathf.Repeat(previousAngle + (Random.Range(0,2) == 0 ? 1 : -1)* Random.Range(45f,180f),360);
            Vector3 temp = (new Vector3(Mathf.Cos(newAngle*Mathf.Deg2Rad),Mathf.Sin(newAngle*Mathf.Deg2Rad),0))*Radius;

            transform.position += temp - offset;
            
            offset = temp;
            previousAngle = newAngle;
            if(Frequency == 0)
            {
                ResetPos();
                yield return new WaitWhile(() => Frequency == 0);
            }
            else
            {
                yield return new WaitForSeconds(1/Frequency);
            }
        }
    }
}
