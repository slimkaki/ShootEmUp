using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable {

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

    // float angle = 0;

    private void FixedUpdate() {
        // angle += 0.1f;
        // Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
        // float x = Mathf.Sin(angle);
        // float y = Mathf.Cos(angle);

        // Thrust(x, y);
       
    }
}
