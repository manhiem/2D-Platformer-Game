using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D playerCollider;
    private Vector2 colliderOffset;
    private Vector2 colliderSize;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        colliderOffset = playerCollider.offset;
        colliderSize = playerCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetFloat("Vertical", Mathf.Abs(vertical));


        MoveAnimation(horizontal);
        CrouchFunctionality(horizontal);
    }

    private void MoveAnimation(float horizontal)
    {
        // Move Animation
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1.0f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        // Move Player
        Vector3 curPos = transform.position;
        curPos.x += horizontal * moveSpeed * Time.deltaTime;
        transform.position= curPos;
    }

    private void CrouchFunctionality(float horizontal)
    {
        // Crouch Animation
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            horizontal = 0;
            animator.SetBool("Crouch", true);
            playerCollider.offset = new Vector2(-0.1658681f, 0.5918809f);
            playerCollider.size = new Vector2(0.8149413f, 1.301477f);
        }
        if (horizontal != 0 || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);
            playerCollider.offset = colliderOffset;
            playerCollider.size = colliderSize;
        }
    }
}
