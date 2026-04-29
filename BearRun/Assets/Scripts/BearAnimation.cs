using UnityEngine;
using UnityEngine.AI;

public class BearAnimation : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public float chaseDistance = 100f;
    public float alertDistance = 10f;
    public AudioClip alertSound;
    private AudioSource audioSource;

    private bool isChasing = false;
    private bool hasPlayedAlertSound = false;

    public float initialSpeed = 3f;
    public float speedIncreaseRate = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent.speed = initialSpeed;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            agent.SetDestination(player.position);
            if (!isChasing)
            {
                isChasing = true;
                animator.SetBool("IsChasing", true);
            }
            IncreaseSpeed();
        }
        else
        {
            agent.ResetPath();
            if (isChasing)
            {
                isChasing = false;
                animator.SetBool("IsChasing", false);
            }
        }

        if (distanceToPlayer <= alertDistance)
        {
            if (!hasPlayedAlertSound)
            {
                PlayAlertSound();
                hasPlayedAlertSound = true;
            }
        }
        else
        {
            hasPlayedAlertSound = false;
        }

        float agentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", agentSpeed);
    }

    private void PlayAlertSound()
    {
        if (alertSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(alertSound);
        }
    }

    private void IncreaseSpeed()
    {
        agent.speed += speedIncreaseRate * Time.deltaTime;
    }
}
