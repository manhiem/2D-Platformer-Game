using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed;
    public Vector2 colliderOffset;
    public Vector2 colliderSize;

    // Jump Vars
    public Rigidbody2D rigidBody;
    public float jumpForce;
    public bool isJump = false;

    // Health
    public List<Image> lifeSprite = new List<Image>();
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        endScreen = GameObject.Find("Canvas/GameOver");
        endScreen.SetActive(false);
        colliderOffset = GetComponent<BoxCollider2D>().offset;
        colliderSize = GetComponent<BoxCollider2D>().size;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetFloat("Vertical", Mathf.Abs(vertical));

        MoveController(horizontal);
        CrouchController(horizontal);
        JumpController(vertical);
    }

    private void MoveController(float horizontal)
    {
        // Move Animation
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1.0f * Mathf.Abs(horizontal);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(horizontal);
        }
        transform.localScale = scale;

        // Move Physics
        Vector3 curPos = transform.position;
        curPos.x += horizontal * moveSpeed * Time.deltaTime;
        transform.position = curPos;
    }

    private void CrouchController(float horizontal)
    {
        Vector2 oldColliderOffset = colliderOffset;
        Vector2 oldColliderSize = colliderSize;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            moveSpeed = 0;
            animator.SetBool("Crouch", true);

            // Change Collider
            colliderOffset = new Vector2(-0.07312155f, 0.60546f);
            colliderSize = new Vector2(0.8352113f, 1.376446f);
        }
        if (horizontal != 0 || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);

            // Change Collider
            colliderOffset = new Vector2(-0.02883071f, 0.9818994f);
            colliderSize = new Vector2(0.6313068f, 2.129324f);
        }
    }

    private void JumpController(float vertical)
    {
        if (!isJump && vertical > 0) //&& rigidBody.velocity.y == 0
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
        
        if(other.gameObject.GetComponent<EnemyController>() != null)
        {
            if (lifeSprite.Count!=0)
            {
                Image lastImage = lifeSprite[lifeSprite.Count - 1];
                Destroy(lastImage);
                lifeSprite.RemoveAt(lifeSprite.Count - 1);
            }

            if(lifeSprite.Count == 0)
            {
                endScreen.SetActive(true);
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = true;
        }
    }
}