using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    public static ShakeCamera instance;


    private void Awake()
    {
        if (instance == null) instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    public void shake() {
      
        StartCoroutine(ShakeCam(0.2f, 0.4f));
    }

    IEnumerator ShakeCam(float duration, float magnitude ) {
        Vector3 originalPos = transform.position;
        float elapsed = 0;
        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition += new Vector3(x, y, 0f);
            
            elapsed += Time.deltaTime;
            yield return null;

        }


        transform.localPosition = originalPos;
       
    }

}
