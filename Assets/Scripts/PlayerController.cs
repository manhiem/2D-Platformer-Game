using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D playerCollider;
    private Vector2 colliderOffset;
    private Vector2 colliderSize;
    // Start is called before the first frame update
    void Start()
    {
        colliderOffset = playerCollider.offset;
        colliderSize = playerCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(speed));
        animator.SetFloat("Vertical", Mathf.Abs(vertical));

        // Move Animation
        Vector3 scale = transform.localScale;
        if(speed < 0) {
            scale.x = -1.0f * Mathf.Abs(scale.x);
        }
        else if (speed > 0) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        // Crouch Animation
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = 0;
            animator.SetBool("Crouch", true);
            playerCollider.offset = new Vector2(-0.1658681f, 0.5918809f);
            playerCollider.size = new Vector2(0.8149413f, 1.301477f);
        }
        if(speed!=0 || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);
            playerCollider.offset = colliderOffset;
            playerCollider.size = colliderSize;
        }
    }
}
