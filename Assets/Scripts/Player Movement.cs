using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Rigidbody playerRigid;
    [SerializeField] private Transform playerTrans;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float walkbackSpeed = 1.5f;
    [SerializeField] private float originalWalkSpeed;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float rotationSpeed = 100f;

    private bool walking = false;

    void Start()
    {
        originalWalkSpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        HandleAnimation();
        HandleRotation();
        HandleRunning();
    }

    void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * walkSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward * walkbackSpeed;
        }

        // Move using Rigidbody for better physics
        playerRigid.MovePosition(playerRigid.position + movement * Time.fixedDeltaTime);
    }

    void HandleAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
        }
    }

    void HandleRotation()
    {
        float rotationDirection = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            rotationDirection = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationDirection = 1f;
        }

        if (rotationDirection != 0f)
        {
            playerTrans.Rotate(0, rotationDirection * rotationSpeed * Time.deltaTime, 0);
        }
    }

    void HandleRunning()
    {
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                walkSpeed = originalWalkSpeed + runSpeed;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                walkSpeed = originalWalkSpeed;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
    }

}
