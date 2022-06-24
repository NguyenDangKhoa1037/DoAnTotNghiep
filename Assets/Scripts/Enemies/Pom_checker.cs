using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pom_checker : MonoBehaviour
{
    private Pom pom;
    private bool isTrigged;
    private bool firtTimes;
    public Pom Pom { get => pom; set => pom = value; }
    public bool IsTrigged { get => isTrigged; set => isTrigged = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!firtTimes)
            Pom.startCounting();
            IsTrigged = true;
            firtTimes = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTrigged)
            isTrigged = false;
    }
}
