using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInput
{
    P1 = 1,
    P2 = 2
}

public class Movement2D : MonoBehaviour
{
    private float moveSpeed;

    private string horizontalAxis;
    private string verticalAxis;

    private string fire1Btn;
    private string fire2Btn;
    private string fire3Btn;

    bool isDoneSetup;


    public System.Action OnClickFire1Action;
    public System.Action OnClickFire2Action;

    // Update is called once per frame
    void Update()
    {
        if (isDoneSetup)
        {
            if (Input.GetAxisRaw(horizontalAxis) != 0 || Input.GetAxisRaw(verticalAxis) != 0)
                Move();

            if (Input.GetButtonUp(fire1Btn))
                if (OnClickFire1Action != null)
                    OnClickFire1Action.Invoke();

            if (Input.GetButtonUp(fire2Btn))
                if (OnClickFire2Action != null)
                    OnClickFire2Action.Invoke();
        }
    }

    private void Move()
    {
        Vector3 rightMovement = Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxisRaw(horizontalAxis);
        Vector3 upMovement = Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxisRaw(verticalAxis);
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.position += rightMovement;
        transform.position += upMovement;

       //UpdateAnimation(heading);
    }
    
    public void SetPlayerInput(PlayerInput playerInput)
    {
        switch (playerInput)
        {
            case PlayerInput.P1:
                {
                    horizontalAxis = "P1_Horizontal";
                    verticalAxis = "P1_Vertical";

                    fire1Btn = "P1_Fire1";
                    fire2Btn = "P1_Fire2";
                    fire3Btn = "P1_Fire3";
                }
                break;
            case PlayerInput.P2:
                {
                    horizontalAxis = "P2_Horizontal";
                    verticalAxis = "P2_Vertical";

                    fire1Btn = "P2_Fire1";
                    fire2Btn = "P2_Fire2";
                    fire3Btn = "P2_Fire3";
                }
                break;
            default:
                {
                    horizontalAxis = "Horizontal";
                    verticalAxis = "Vertical";

                    fire1Btn = "Fire1";
                    fire2Btn = "Fire2";
                    fire3Btn = "Fire3";
                }
                break;
        }
        isDoneSetup = true;
    }
    
    public void SetMovementSpeed( float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
