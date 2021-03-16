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

    private void FixedUpdate() {
        
    }
}
