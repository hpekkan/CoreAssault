using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuObject : MonoBehaviour
{
    public float rotateSpeed=50f;
    Transform transform;
    void Start()
    { 
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime,Space.World);
    }
}
