using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBall : MonoBehaviour
{
    //defining ball movement
    private Vector3 _velocity;
    public float Speed = 5;

    //setup for ball
    public void BallReset(){
        transform.position = Vector3.zero;
        _velocity = new Vector3(Random.Range(-1f,1f), -1);
        _velocity = _velocity.normalized;
    }

    //bounce for ball hitting something vertically
    public void BounceHitVertical(){
        _velocity.x*=-1;
    }

    //bounce for ball hitting something horizontally
    public void BounceHitHorizontal(){
        _velocity.y*=-1;

    }

    // Start is called before the first frame update
    void Start(){
        BallReset();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=_velocity*Time.deltaTime*Speed;

        //Check all the bounds
        if(Mathf.Abs(transform.position.x) > GameController.Instance.XBound){
            //Hit left or right of screen
            BounceHitVertical();

            //reset location
            Vector3 location = transform.position;
            if(location.x<0){
                location.x = GameController.Instance.XBound*-1;
            }
            else{
                location.x = GameController.Instance.XBound;
            }
            transform.position = location;
        }
        if (transform.position.y>GameController.Instance.YBound){
            //Hit top of screen
            BounceHitHorizontal();
            BounceHitVertical();

            //reset location
            Vector3 location = transform.position;
            location.y = GameController.Instance.YBound;
            transform.position = location;
        }
        else if (transform.position.y<-1*GameController.Instance.YBound){
            //Went off screen below
            BallReset();

            //Lose a life?
        }

    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            BounceHitHorizontal();
            BreakoutPaddle paddle = other.transform.gameObject.GetComponent<BreakoutPaddle>();

            _velocity.x+=paddle.Velocity*Speed;
        }
        else{
            //Hit block
            Destroy(other.transform.gameObject);
            BounceHitHorizontal();

            //Update score?
        }
        
    }
}
