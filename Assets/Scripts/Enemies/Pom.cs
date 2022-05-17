using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
public class Pom : Enemy.Enemy
{
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private float radiusExplode;
    private int state = 0;
    public static int SLEEP = 0;
    public static int COUNTER = 1;
    public static int EXPLODE = 2;

    private bool isExplode = false;
    public int State { get => state; set => state = value; }


    private void FixedUpdate()
    {
        if (!isExplode)
        {
            bool check = false;
            check = Physics2D.OverlapCircle(transform.position, radiusExplode, layerMaskPlayer) != null;
            if (check)
            {
                isExplode = true;
                State = EXPLODE;
            }
        }
    }


}
