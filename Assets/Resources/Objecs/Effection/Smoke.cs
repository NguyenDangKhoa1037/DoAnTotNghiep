using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] GameObject smokeObj;
    [SerializeField] float timeExists = 4f;
    private void Start()
    {
        Destroy(gameObject, timeExists);
    }



}
