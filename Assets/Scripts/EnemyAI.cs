using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public float attackRange = 2f; // Range at which the enemy attacks
    public float visionRange = 10f; // Maximum distance at which the AI can see the player

    public Animator animator; // Reference to the Animator component
    public string walkAnimationTrigger = "Walk"; // Name of the walk animation trigger parameter
    public string idleAnimationTrigger = "Idle"; // Name of the idle animation trigger parameter

    private Rigidbody enemyRigidbody;
    private RaycastHit groundHit;
    private bool isWalking = false; // Flag to track if the AI is currently walking

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        enemyRigidbody.freezeRotation = true; // Prevents rotation due to physics interactions
        enemyRigidbody.useGravity = false; // Disables gravity to prevent bouncing

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // Check if player is within vision range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= visionRange)
        {
            // Move towards the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            enemyRigidbody.velocity = direction * moveSpeed;

            // Trigger walk animation if not already walking
            if (!isWalking)
            {
                animator.SetTrigger(walkAnimationTrigger);
                isWalking = true;
            }

            // Smoothly rotate to face the movement direction (except for Y-axis rotation)
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            enemyRigidbody.MoveRotation(Quaternion.Slerp(enemyRigidbody.rotation, targetRotation, Time.deltaTime * 5f));

            // Rotate the upper body to face the player
            Vector3 upperBodyDirection = player.position - transform.position;
            upperBodyDirection.y = 0f;
            if (upperBodyDirection != Vector3.zero)
            {
                Quaternion upperBodyRotation = Quaternion.LookRotation(upperBodyDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, upperBodyRotation, Time.deltaTime * 5f);
            }

            // Attack the player when in range
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Stop moving if player is out of vision range
            enemyRigidbody.velocity = Vector3.zero;

            // Trigger idle animation if not already idle
            if (isWalking)
            {
                animator.SetTrigger(idleAnimationTrigger);
                isWalking = false;
            }
        }

        // Ground the AI to prevent bouncing
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit))
        {
            float distanceToGround = Vector3.Distance(transform.position, groundHit.point);
            if (distanceToGround > 0.1f)
            {
                transform.position -= Vector3.up * (distanceToGround - 0.1f);
            }
        }
    }

    private void AttackPlayer()
    {
        // Add your attack logic here
        Debug.Log("Attacking the player!");
    }
}
