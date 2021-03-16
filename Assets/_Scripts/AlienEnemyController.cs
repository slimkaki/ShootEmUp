using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemyController : SteerableBehaviour, IShooter, IDamageable {

    public AudioClip shootSFX;
    public GameObject tiro;
    public GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    public void Shoot() {
        AudioManager.PlaySFX(shootSFX);
        Instantiate(tiro, transform.position, Quaternion.identity);
    }

    public void TakeDamage() {
        gm.pontos += 10;
        Die();
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        if(gm.gameState == GameManager.GameState.MENU || gm.gameState == GameManager.GameState.ENDGAME) {
            Die();
        }
        if (gameObject.transform.position.x < GameObject.FindWithTag("Player").transform.position.x - 25.0f) {
            Die();
        }
    }
}
