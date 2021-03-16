using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemySpawner : SteerableBehaviour {
    public GameObject Alien;
    public GameManager gm;
    private float last_spawn = 0.0f;
    // private int alien_count = 0;
    void Start() {
        gm = GameManager.GetInstance();
        for (int i = 0; i < 2; i++) {
            float randomY = Random.Range(-5.0f, 5.0f);
            float randomX = Random.Range(10.0f, 20.0f);
            Instantiate(Alien, new Vector3(randomX, randomY, 0.0f), Quaternion.identity, transform);
            // alien_count++;
        }
    }

    private void Spawn() {
        float randomY = Random.Range(5.0f, -5.0f);
        Instantiate(Alien, new Vector3(GameObject.FindWithTag("Player").transform.position.x, 0.0f, 0.0f), Quaternion.identity, transform);
        // alien_count++;
        last_spawn = Time.time;
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (Time.time - last_spawn >= 3.0f) {//|| alien_count <= 2) {
            Spawn();
        }
    }
}
