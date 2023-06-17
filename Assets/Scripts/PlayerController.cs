﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            horizontal = 0;
            animator.SetBool("Crouch", true);
            // Change Collider
        }
        if (horizontal != 0 || Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);
            // Change Collider
        }
    }

    private void JumpController(float vertical)
    {
        if (!isJump && vertical > 0 && rigidBody.velocity.y == 0)
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
            isJump = true;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = false;
        }
        
        if(other.gameObject.GetComponent<EnemyController>() != null)
        {
            Image lastImage = lifeSprite[lifeSprite.Count - 1];
            Destroy(lastImage);
            lifeSprite.RemoveAt(lifeSprite.Count - 1);

            if(lifeSprite.Count == 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJump = true;
        }
    }
}