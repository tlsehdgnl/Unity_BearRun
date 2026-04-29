using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float runSpeedMultiplier = 2f;
    public float rotationSpeed = 200f;
    public float jumpForce = 10f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private Quaternion initialRotation;
    private bool isGameOver = false;

    public AudioClip deathSound;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = transform.rotation;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        float moveDirection = Input.GetAxis("Vertical");
        float rotationDirection = Input.GetAxis("Horizontal");

        Move(moveDirection);
        Rotate(rotationDirection);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRotation();
        }
    }

    private void Move(float moveDirection)
    {
        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= runSpeedMultiplier;
        }

        Vector3 movement = transform.forward * moveDirection * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void Rotate(float rotationDirection)
    {
        transform.Rotate(Vector3.up, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Bear"))
        {
            isGameOver = true;
            PlayDeathSound();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void ResetRotation()
    {
        transform.rotation = initialRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
