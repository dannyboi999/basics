using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetpos = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocity, smoothTime); 
    }
}
