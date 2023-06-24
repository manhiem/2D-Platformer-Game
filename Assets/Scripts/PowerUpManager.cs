using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private PowerupSelect powerupSelect;

    // Freeze Time Vars
    [SerializeField]
    private EnemyController[] Enemies;

    // Power Up Time Limit
    private bool activatedPowerUp = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!activatedPowerUp)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                if(powerupSelect.powerUp == PowerUps.TimeFreeze)
                {
                    StartCoroutine(TimeFreeze());
                }
                else if (powerupSelect.powerUp == PowerUps.DoubleJump)
                {
                    StartCoroutine(DoubleJump(collision.gameObject));
                }
                else if (powerupSelect.powerUp == PowerUps.SpeedBoost)
                {
                    StartCoroutine(SpeedBoost(collision.gameObject));
                }
                else if (powerupSelect.powerUp == PowerUps.Invincibility)
                {
                    StartCoroutine(Invincibility(collision.gameObject));
                }
            }
            
        }
    }

    IEnumerator TimeFreeze()
    {
        foreach (EnemyController e in Enemies)
        {
            e.DisableMovement();
        }

        Debug.Log("Disabled Movement");

        yield return new WaitForSeconds(7f);

        foreach (EnemyController e in Enemies)
        {
            e.EnableMovement();
        }

        Debug.Log("Enabled Movement");
    }

    IEnumerator DoubleJump(GameObject Player)
    {
        float jumpInfo = Player.GetComponent<PlayerController>().GetJumpForce();
        Player.GetComponent<PlayerController>().SetJumpForce(jumpInfo*1.5f);

        yield return new WaitForSeconds(7f);

        Player.GetComponent<PlayerController>().SetJumpForce(jumpInfo);
    }

    IEnumerator SpeedBoost(GameObject Player)
    {
        float speedInfo = Player.GetComponent<PlayerController>().GetMoveSpeed();
        Player.GetComponent<PlayerController>().SetMoveSpeed(speedInfo * 1.5f);

        yield return new WaitForSeconds(7f);

        Player.GetComponent<PlayerController>().SetMoveSpeed(speedInfo);
    }

    IEnumerator Invincibility(GameObject Player)
    {
        Player.GetComponent<PlayerController>().Invincible = true;

        yield return new WaitForSeconds(7f);

        Player.GetComponent<PlayerController>().Invincible = false;
    }
}

[Serializable]
public class PowerupSelect
{
    public PowerUps powerUp;
}

public enum PowerUps
{
    TimeFreeze,
    DoubleJump,
    SpeedBoost,
    Invincibility
}
