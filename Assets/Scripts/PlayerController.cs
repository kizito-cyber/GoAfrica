using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 2.0f;
    private const float TURN_SPEED = 0.05f;


    private bool isDriving=false;
    //Movement
    private CharacterController controller;
    private float verticalVelocity;
    
    // Start is called before the first 
    private int desiredLane = 1;//0 = Left 2=Rightframe update
    //Speed Modifier
    private float originalSpeed = 17.0f;
    private float speed = 20.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f; 
    private float speedIncreaseAMount = 0.1f;
    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!isDriving)
        {
            return;   
        }
        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAMount;
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }
        if(MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
        }
        if(MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
        }

        
        
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        Vector3 dir = controller.velocity;
        if(dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward,dir,TURN_SPEED);
        }
       

    }
    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane,0,2);
    }
    public void StartDriving()
    {
        isDriving = true;

    }
    private void Crash()
    {
        isDriving = false;
        GameManager.Instance.IsDead = true;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
            break;
        }
    }
}
