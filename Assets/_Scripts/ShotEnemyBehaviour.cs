using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyBehaviour : SteerableBehaviour {

    private Vector3 direction;

    public GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inimigos")) return;
        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (!(damageable is null)) {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

    void Update() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        Vector3 posPlayer = GameObject.FindWithTag("Player").transform.position;
        direction = (posPlayer - transform.position).normalized;
        float dist = Vector2.Distance(posPlayer, transform.position);
        if (dist > 15.0f) {
            Destroy(gameObject);
        }
        Thrust(direction.x, direction.y);
    }

    private void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
