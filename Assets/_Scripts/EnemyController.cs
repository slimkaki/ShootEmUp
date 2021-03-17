using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable {

    public AudioClip shootSFX;
    public GameObject tiro;
    public GameManager gm;
    private float last_shoot;
    public GameObject floatingPoints;

    void Start() {
        gm = GameManager.GetInstance();
        last_shoot = Time.time;
    }

    public void Shoot() {
        if (Time.time - last_shoot <= 1.0f) return;
        AudioManager.PlaySFX(shootSFX);
        Instantiate(tiro, transform.position, Quaternion.identity);
        last_shoot = Time.time;
    }

    public void TakeDamage() {
        Instantiate(floatingPoints, transform.position, Quaternion.identity);
        gm.pontos += 10;
        Die();
    }

    public void Die() {
        Destroy(transform.parent.gameObject);
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gameObject.transform.position.x < GameObject.FindWithTag("Player").transform.position.x - 25.0f) {
            Die();
        }
    }

}
