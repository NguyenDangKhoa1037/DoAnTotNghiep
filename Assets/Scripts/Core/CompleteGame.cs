using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGame : MonoBehaviour
{
    [SerializeField] private GameObject information;
    public static CompleteGame instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        information.gameObject.SetActive(false);
    }
    public void complete() {
        information.gameObject.SetActive(true);
    }

    public void nextLevel()
    {
        DemoMap.instance.nextLevel();
    }
}
