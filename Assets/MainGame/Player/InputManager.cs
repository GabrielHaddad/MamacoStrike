using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public const float MouseScrollCDDuration = 0.4f;


    public static InputManager Instance {
        get
        {
            if(!m_instance)
            {
                m_instance = FindObjectOfType<InputManager>();

                if(!m_instance)
                {
                    m_instance = (new GameObject("InputManager")).AddComponent<InputManager>();
                }

                // m_instance.Init();
            }

            return m_instance;
        }
    }

    public static Vector2 MovementVector
    {
        get
        {
            return Instance.movVect;
        }
    }

    [HideInInspector]
    public UnityEvent OnShoot = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSwap = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnBtn1 = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnBtn2 = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnBtn3 = new UnityEvent();

    private Vector2 movVect = Vector2.zero;


    private bool mouseScrollOnCD = false;

    // private bool initted = false;

    private static InputManager m_instance = null;
    
    private void Start()
    {
        if(!m_instance)
        {
            m_instance = this;
            // Init();
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
        if(Input.GetKey(KeyCode.Mouse0)) OnShoot?.Invoke();

        if(Input.GetKeyDown(KeyCode.Mouse1)) OnBtn1?.Invoke();
        if(Input.GetKeyDown(KeyCode.Space)) OnBtn2?.Invoke();
        if(Input.GetKeyDown(KeyCode.R)) OnBtn3?.Invoke();

        if(!mouseScrollOnCD ? !Mathf.Approximately(Input.mouseScrollDelta.y,0) : false)
        {
            OnSwap?.Invoke();

            StartCoroutine(MouseScrollCooldown());
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Q)) OnSwap?.Invoke();
        }

        movVect = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }

    private IEnumerator MouseScrollCooldown()
    {
        mouseScrollOnCD = true;

        yield return new WaitForSeconds(MouseScrollCDDuration);

        mouseScrollOnCD = false;
    }
}
