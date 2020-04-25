using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    AudioSource audioSource;

    [SerializeField]
    float speed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ApplyMovementInput();
    }

    private void ApplyMovementInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime * 100;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime * 150;
        }

        MovementAnimationUpdate(moveHorizontal, moveVertical);
    }

    /// <summary>
    /// Updates the [Player's Animation]
    /// based on    [Player's Movement].
    /// </summary>
    /// <param name="moveX"></param>
    /// <param name="moveY"></param>
    private void MovementAnimationUpdate(float moveX, float moveY)
    {
        // Changes Animation Based on direction facing.
        animator.SetFloat("FaceX", moveX);
        animator.SetFloat("FaceY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetBool("isWalking", true);
            if (moveX > 0) animator.SetFloat("LastMoveX", 1f);
            else if (moveX < 0) animator.SetFloat("LastMoveX", -1f);
            else animator.SetFloat("LastMoveX", 0f);

            if (moveY > 0) animator.SetFloat("LastMoveY", 1f);
            else if (moveY < 0) animator.SetFloat("LastMoveY", -1f);
            else animator.SetFloat("LastMoveY", 0f);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
