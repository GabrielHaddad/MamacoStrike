using UnityEngine;

public class Spin : MonoBehaviour
{
    public float Speed;

    // [HideInInspector]
    public bool yAxis = false;

    private Vector3 vect;
    
    private void Start()
    {
        vect = yAxis ? Vector3.up : Vector3.forward;
    }

    private void Update()
    {
        if(gameObject.activeSelf) transform.Rotate(vect*Speed*Time.deltaTime);
    }
}