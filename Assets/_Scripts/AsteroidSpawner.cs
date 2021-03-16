using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : SteerableBehaviour {

    public AudioClip asteroidSFX;
    // public GameObject tiro;
    public GameManager gm;
    public GameObject asteroid;
    private float last_spawn = 0.0f;
    // public int asteroid_count = 0;

    void Start() {
        gm = GameManager.GetInstance();
        for (int i = 0; i < 3; i++) {
            float randomY = Random.Range(-5.0f, 5.0f);
            float randomX = Random.Range(10.0f, 20.0f);
            Instantiate(asteroid, new Vector3(randomX, randomY, 0.0f), Quaternion.identity);
            // asteroid_count++;
        }
    }

    private void Spawn() {
        float randomY = Random.Range(5.0f, -5.0f);
        Instantiate(asteroid, new Vector3(GameObject.FindWithTag("Player").transform.position.x + 20.0f, randomY, 0.0f), Quaternion.identity);
        // asteroid_count++;
        last_spawn = Time.time;
    } 

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - last_spawn >= 8.0f && GameObject.FindGameObjectsWithTag("Asteroide").Length <= 5) {
            Spawn();
        }
    }
}
