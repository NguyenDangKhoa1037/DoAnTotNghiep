using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] float smoothSpeed = 0.2f;
    [SerializeField] private Transform target;

    public Transform Target { get => target; set => target = value; }
    private void LateUpdate()
    {
        if (Target == null) return;
        Vector3 dir = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(transform.position, dir, smoothSpeed);
       
        transform.position = smoothPos;
        //transform.LookAt(target);
    }
}
