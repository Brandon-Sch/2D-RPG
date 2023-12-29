using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    private WaveSpawner waveSpawnerInstance;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push]
    protected Vector3 pushDirection;

    private void Start()
    {
        waveSpawnerInstance = FindObjectOfType<WaveSpawner>();
    }

    //All fighters can receive damage/die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 50, 0.5f);


            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }

    }

    protected virtual void Death()
    {
        if (waveSpawnerInstance != null)
        {
            waveSpawnerInstance.setEnemyCount(0);
        }

        // Deactivate the current player GameObject
        gameObject.SetActive(false);

        // Teleport the player to the first scene
        GameManager.instance.SaveState();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        // Reactivate the player GameObject if needed
        gameObject.SetActive(true);
        hitpoint = 10;
    }
}