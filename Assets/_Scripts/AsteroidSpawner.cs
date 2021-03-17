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
        StartSpawn();
    }

    void StartSpawn() {
        for (int i = 0; i < 3; i++) {
            float randomX = Random.Range(20.0f, 30.0f);
            float randomY = Random.Range(-5.0f, 5.0f);
            Instantiate(asteroid, new Vector3(GameObject.FindWithTag("Player").transform.position.x + randomX,
                                              GameObject.FindWithTag("Player").transform.position.y + randomY, 0.0f), 
                                              Quaternion.Euler(0f, 180f, 0f), transform);
        }
        last_spawn = Time.time;
    }

    private void Spawn() {
        float randomY = Random.Range(5.0f, -5.0f);
        Instantiate(asteroid, new Vector3(GameObject.FindWithTag("Player").transform.position.x + 20.0f, randomY, 0.0f), Quaternion.identity);
        // asteroid_count++;
        last_spawn = Time.time;
    } 

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gm.ResetFlag) {
            GameObject[] ast = GameObject.FindGameObjectsWithTag("Asteroide");
            foreach(GameObject a in ast) {
                Destroy(a);
            }
            StartSpawn();
            gm.ResetFlag = false;
        }
        if(Time.time - last_spawn >= 3.5f && GameObject.FindGameObjectsWithTag("Asteroide").Length <= 7) {
            Spawn();
        }
    }
}
