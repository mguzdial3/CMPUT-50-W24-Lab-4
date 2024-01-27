using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutPaddle : MonoBehaviour
{
    //Paddle velocity (just used to track movement since position defined by mouse)
    private float _velocity;
    public float Velocity { get { return _velocity; } }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 paddlePos = transform.position;
        float currPaddleX = paddlePos.x;
        mousePos.z = paddlePos.z - Camera.main.transform.position.z; //distance of camera to paddle

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos); //world position of ball

        float mouseX = mouseWorldPos.x;

        if(Mathf.Abs(mouseX)<GameController.Instance.XBound){
            paddlePos.x = mouseX;
            transform.position = paddlePos;
        }

        _velocity = paddlePos.x-currPaddleX;
    }

    
}
