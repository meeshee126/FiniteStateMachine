using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds Components to the gameObject when script Component is inserted
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Entity : MonoBehaviour
{
    [Header("Behaviour Configurations")]
    //Set target = Player
    public GameObject target;
    public GameObject facingDirection;
    public float speed;
    
    [Space(10)]
    [Header("Radiuses")]
    [Space]
    [Range(0.5f, 9f)]
    public float observationRadius;
    [Range(0.5f, 9f)]
    public float chaseRadius, lostRadius;
    [SerializeField]
    private bool showObservationRadius, showAttackRadius, showLostRadius;

    [Space(10)]
    [Header("Chase checks")]
    [Space]
    public bool chase;
    public bool idle;

    [Space(10)]
    [Header("Timer")]
    [Space]
    public float waitTime;

    [Space(10)]
    [Header("Other..")]
    [Space]
    public Animator characterAnimator;
    public Animator charIconAnimator;
    public Rigidbody2D rb;
    public AudioSource audioSource;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        charIconAnimator = transform.GetChild(1).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ApplyMovementInput();   
    }

    private void ApplyMovementInput()
    {
        float moveHorizontal = rb.velocity.x;
        float moveVertical = rb.velocity.y;

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
        characterAnimator.SetFloat("FaceX", moveX);
        characterAnimator.SetFloat("FaceY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            characterAnimator.SetBool("isWalking", true);
            if (moveX > 0) characterAnimator.SetFloat("LastMoveX", 1f);
            else if (moveX < 0) characterAnimator.SetFloat("LastMoveX", -1f);
            else characterAnimator.SetFloat("LastMoveX", 0f);

            if (moveY > 0) characterAnimator.SetFloat("LastMoveY", 1f);
            else if (moveY < 0) characterAnimator.SetFloat("LastMoveY", -1f);
            else characterAnimator.SetFloat("LastMoveY", 0f);
        }
        else
        {
            characterAnimator.SetBool("isWalking", false);
        }
    }

    /// <summary>
    /// Draws The Apropriate Gizmos
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (showObservationRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, observationRadius);
        }
        if (showAttackRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, chaseRadius);
        }
        if (showLostRadius)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, lostRadius);
        }
    }
}
