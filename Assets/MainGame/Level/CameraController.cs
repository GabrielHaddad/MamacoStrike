using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType<CameraController>();

                if(!m_instance)
                {
                    m_instance = Camera.main.gameObject.AddComponent<CameraController>();
                }

                m_instance?.Init();
            }

            return m_instance;
        }
    }

    private static CameraController m_instance = null;

    public float LerpDistanceToPlayer = 0.35f;
    public float DistanceThreshold = 0.3f;
    public float LerpSpeed = 9;
    public float ScreenShakeFrequency = 20;
    
    [HideInInspector]
    public float currentShakeIntensity = 0;
    
    private Camera mainCamera = null;
    private Transform cameraTarget = null;
    private Vector3 offset;
    private bool initted = false;

    public void ScreenShake(float Intensity = 1)
    {
        if(Intensity <= 0) return;

        currentShakeIntensity += Intensity;
    }

    private void Start()
    {
        if(m_instance == null)
        {
            m_instance = this;
            Init();
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(this);
            }
        }
    }

    private void Update()
    {
        if(PlayerController.Instance)
        {
            cameraTarget.position = Vector2.Lerp(
                PlayerController.Instance.transform.position,
                mainCamera.ScreenToWorldPoint(Input.mousePosition),
                LerpDistanceToPlayer);
        }

        if(Vector2.Distance(cameraTarget.position,transform.position) > DistanceThreshold)
        {
            Vector2 targetPos = Vector2.Lerp(
                transform.position,
                cameraTarget.position,
                LerpSpeed * Time.deltaTime);

            transform.position = new Vector3(targetPos.x,targetPos.y,transform.position.z);
        }

        // if(Input.GetKeyDown(KeyCode.Z))
        // {
        //     currentShakeIntensity += 1;
        // }
    }

    private void Init()
    {
        if(initted) return;

        initted = true;

        mainCamera = Camera.main;

        cameraTarget = (new GameObject("Camera Target")).transform;
        cameraTarget.SetParent(transform);

        StartCoroutine(ScreenShakeCoroutine());
    }


    private IEnumerator ScreenShakeCoroutine()
    {
        while(true)
        {
            yield return new WaitWhile(() => currentShakeIntensity == 0);
            
            float previousAngle = 0;
            offset = Vector3.zero;

            while(currentShakeIntensity > 0.005f)
            {
                float newAngle = Mathf.Repeat(previousAngle + (Random.Range(0,2) == 0 ? 1 : -1)* Random.Range(45f,180f),360);
                Vector3 temp = (new Vector3(Mathf.Cos(newAngle*Mathf.Deg2Rad),Mathf.Sin(newAngle*Mathf.Deg2Rad),0))*currentShakeIntensity;

                transform.position += temp - offset;
                
                offset = temp;
                previousAngle = newAngle;
                
                currentShakeIntensity *= 0.5f;

                if(ScreenShakeFrequency == 0)
                {
                    yield return new WaitWhile(() => ScreenShakeFrequency == 0);
                }
                else
                {
                    yield return new WaitForSeconds(1/ScreenShakeFrequency);
                }
            }

            currentShakeIntensity = 0;
        }
    }
}
