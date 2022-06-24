using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectionManager : MonoBehaviour
{
    private List<string> effections = new List<string>();
    public static EffectionManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public List<string> Effections { get => effections; }
    public bool canPushed(string effection) {
        int index = effections.IndexOf(effection);
        return index == -1;
    }

    public delegate void CreateEffection(EffectionManager eff);
    public void addEffect(CreateEffection create) {
        create(this);
    }
}
