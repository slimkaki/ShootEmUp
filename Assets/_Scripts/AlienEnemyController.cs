using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemyController : SteerableBehaviour, IShooter, IDamageable {

    public AudioClip shootSFX;
    public GameObject tiro;
    public GameManager gm;
    private Vector3 prevPosition;

    void Start() {
        gm = GameManager.GetInstance();
        prevPosition = transform.position;
    }

    public void Shoot() {
        AudioManager.PlaySFX(shootSFX);
        Instantiate(tiro, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet")) {
            gm.pontos += 10;
            Die();
        }
    }
    public void TakeDamage() {
        
        
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gameObject.transform.position.x < GameObject.FindWithTag("Player").transform.position.x - 25.0f) {
            Die();
        }
        Vector3 direction = transform.position - prevPosition;
        direction.Normalize();
        if ((transform.position.y > 4.5f && direction.y > 0) || (transform.position.y < 4.9f && direction.y < 0)) {
            Thrust(direction.x, -direction.y);
        }
        prevPosition = transform.position;
    }
    
}
