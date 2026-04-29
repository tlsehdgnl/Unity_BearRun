using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float speed = new Vector3(moveX, 0, moveZ).magnitude;

        animator.SetFloat("Speed", speed);
    }
}
