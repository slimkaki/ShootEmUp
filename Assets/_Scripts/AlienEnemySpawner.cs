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
        StartSpawn();
    }

    void StartSpawn() {
        for (int i = 0; i < 2; i++) {
            float randomX = Random.Range(25.0f, 35.0f);
            float randomY = Random.Range(-5.0f, 5.0f);
            Instantiate(Alien, new Vector3(GameObject.FindWithTag("Player").transform.position.x + randomX,
                                           GameObject.FindWithTag("Player").transform.position.y + randomY, 0.0f), 
                                           Quaternion.Euler(0f, 180f, 0f), transform);
        }
        last_spawn = Time.time;
    }

    private void Spawn() {
        float randomY = Random.Range(5.0f, -5.0f);
        float randomX = Random.Range(15.0f, 25.0f);
        Instantiate(Alien, new Vector3(randomX, randomY, 0.0f), Quaternion.identity, transform);
        // alien_count++;
        last_spawn = Time.time;
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gm.ResetFlag) {
            GameObject[] ene = GameObject.FindGameObjectsWithTag("AlienEnemy");
            foreach(GameObject e in ene) {
                Destroy(e);
            }
            StartSpawn();
        }
        if (Time.time - last_spawn >= 3.0f && GameObject.FindGameObjectsWithTag("AlienEnemy").Length <= 3) {
            Spawn();
        }
    }
}
