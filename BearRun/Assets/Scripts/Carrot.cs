using UnityEngine;

public class Carrot : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip eatSound;
    private AudioSource audioSource;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = eatSound;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Score scoreManager = FindObjectOfType<Score>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            if (audioSource != null && eatSound != null)
            {
                audioSource.Play();
                Destroy(gameObject, eatSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
