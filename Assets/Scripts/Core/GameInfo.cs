using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    private int level = 1;
    public static GameInfo instance;

    public int Level { get => level; set => level = value; }

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
