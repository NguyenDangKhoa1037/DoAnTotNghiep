using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnDesktop : MonoBehaviour
{
    private float speed;
    private Vector2 direct = Vector2.zero;
    public void config(float speed) {
        this.speed = speed;
    }

    private void Update()
    {
        handleInput();
        startMove(direct);
    }

    private void handleInput() {
        if (Input.GetKeyDown(KeyCode.A)) direct.x -= 1;
        if (Input.GetKeyUp(KeyCode.A)) direct.x += 1;

        if (Input.GetKeyDown(KeyCode.D)) direct.x += 1;
        if (Input.GetKeyUp(KeyCode.D)) direct.x -= 1;

        if (Input.GetKeyDown(KeyCode.S)) direct.y -= 1;
        if (Input.GetKeyUp(KeyCode.S)) direct.y += 1;

        if (Input.GetKeyDown(KeyCode.W)) direct.y += 1;
        if (Input.GetKeyUp(KeyCode.W)) direct.y -= 1;
    }
    void resetStickRectPostion() {
        
    }
    private void startMove(Vector2 dir) { 
        dir.Normalize();
        this.transform.Translate(dir * speed * Time.deltaTime);
    }
}
