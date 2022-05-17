using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    Animator animator;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachine;
    private Transform player;
    public static ShakeCamera instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void shake() {
        cinemachine.Follow = null;
        cinemachine.enabled = false;
        animator.Play("shakeCam");
    }

    private void endShake() {
        cinemachine.enabled = true;
        cinemachine.Follow = player;

    }

}
