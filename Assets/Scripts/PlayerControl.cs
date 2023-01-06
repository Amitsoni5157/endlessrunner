using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    int LaneExpected = 1;
    public float LaneDistance = 4;

    private CharacterController _controller;
    private Vector3 _direction;

    public float Forwrdspeed;
    public float JumpSpeed;
    public float Gravity = -20;

    public Transform GroundCheck;
    public LayerMask GroundLayer;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction.z = Forwrdspeed;

        bool isGrounded = Physics.CheckSphere(GroundCheck.position,0.15f,GroundLayer);

        if (isGrounded)
        {
            _direction.y = -0.25f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _direction.y += Gravity + Time.deltaTime;
        }
        playerMove();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LaneExpected++;
            if(LaneExpected == 3)
            {
                LaneExpected = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LaneExpected--;
            if (LaneExpected == -1)
            {
                LaneExpected = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(LaneExpected == 0)
        {
            targetPosition += Vector3.left * LaneDistance;
        }else if (LaneExpected == 2)
        {
            targetPosition += Vector3.right * LaneDistance;
        }

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 10 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            _controller.Move(moveDir);
        else
            _controller.Move(diff);
    }
  

    void playerMove()
    {
        _controller.Move(_direction * Time.deltaTime);
    }

    void Jump()
    {
        _direction.y = JumpSpeed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
