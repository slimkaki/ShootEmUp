using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBackwardsBehaviour : SteerableBehaviour {

    private void Update() {
        float dist = Vector2.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);
        if (dist > 15.0f) {
            Destroy(gameObject);
        }
        Thrust(-1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
       if (collision.CompareTag("Player")) return;
       IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
       if (!(damageable is null)) {
           damageable.TakeDamage();
       }
       Destroy(gameObject);
   }
}
