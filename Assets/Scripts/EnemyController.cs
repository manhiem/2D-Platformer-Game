using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool movingRight = true;

    private void Update()
    {
        // Move Animation and Physics
        Vector3 scale = transform.localScale;
        if (movingRight)
        {
            scale.x = 1.0f;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            scale.x = -1.0f;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        transform.localScale = scale;

        CheckEdges();
    }

    private void CheckEdges()
    {
        if (movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }
    }

    public void DisableMovement()
    {
        moveSpeed = 0f;
    }
    public void EnableMovement()
    {
        moveSpeed = 2f;
    }
}
