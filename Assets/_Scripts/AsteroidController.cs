using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : SteerableBehaviour, IDamageable {

    public AudioClip asteroidSFX;
    // public GameObject tiro;
    public GameManager gm;
    private int speed;
    private float rotSpeed;

    void Start() {
        gm = GameManager.GetInstance();
        speed = Random.Range(-3, -1);
        rotSpeed = Random.Range(-2.5f, -0.2f);
    }

    // public void Shoot() {
    //     AudioManager.PlaySFX(shootSFX);
    //     Instantiate(tiro, transform.position, Quaternion.identity);
    // }

    private void Reset() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Asteroide");
        foreach(GameObject enemy in enemies) GameObject.Destroy(enemy);
        Start();
        gm.ResetFlag = false;
    }

    public void TakeDamage() {
        gm.pontos += 5;
        Die();
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gm.ResetFlag) { Reset(); return;}
        if (gameObject.transform.position.x < GameObject.FindWithTag("Player").transform.position.x - 25.0f) {
            Die();
        }
        Thrust(speed, 0);
        gameObject.transform.Rotate(0.0f, 0.0f, rotSpeed);
    }
}
