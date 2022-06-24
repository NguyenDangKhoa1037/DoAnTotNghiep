using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    [Range(1f, 3f)]
    [SerializeField] float speed = 1;
    [Range(1f, 3f)]
    [SerializeField] float speedBack = 1.3f;
    [SerializeField] private float _orthoraphic = 5f;
    [SerializeField] float sizeZoom;
    private Camera myCam;

    private float _fixOrthoraphic;
    private bool back = false;
    private int state = 0;
    private void Awake()
    {
        myCam = GetComponent<Camera>();
    }
    public void zoom()
    {
        this._fixOrthoraphic = sizeZoom;
        state = 1;
    }

    public void stopZoom()
    {
        state = 2;
    }

    void Update()
    {
        if (state == 1)
        {
            float mySize = myCam.orthographicSize;
            mySize -= Time.deltaTime * speed;
            if (mySize <= _fixOrthoraphic)
            {
                mySize = _fixOrthoraphic;
                state = 0;
            }
            myCam.orthographicSize = mySize;
        }
        else if (state == 2)
        {
            float mySize = myCam.orthographicSize;
            mySize += Time.deltaTime * speedBack;
            if (mySize >= _orthoraphic)
            {
                mySize = _orthoraphic;
                state = 0;
            }
            myCam.orthographicSize = mySize;
        }
    }
}
