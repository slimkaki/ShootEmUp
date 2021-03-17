using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemySpawn : SteerableBehaviour {
    
    public GameManager gm;

    public GameObject Enemy;

    private float last_spawn = 0.0f;
    void Start() {
        gm = GameManager.GetInstance();
        StartSpawn();
    }

    void StartSpawn() {
        Instantiate(Enemy, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, transform);
        Instantiate(Enemy, new Vector3(2.0f, 2.0f, 0.0f), Quaternion.identity, transform);
        Instantiate(Enemy, new Vector3(2.0f, -2.0f, 0.0f), Quaternion.identity, transform);
        last_spawn = Time.time;
    }

    private void Spawn() {
        float randomX = Random.Range(20.0f, 30.0f);
        float randomY = Random.Range(5.0f, -5.0f);
        Instantiate(Enemy, new Vector3(GameObject.FindWithTag("Player").transform.position.x + randomX,
                                       GameObject.FindWithTag("Player").transform.position.y + randomY, 0.0f), 
                                       Quaternion.Euler(0f, 180f, 0f), transform);
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gm.ResetFlag) {
            GameObject[] com = GameObject.FindGameObjectsWithTag("CommonEnemy");
            foreach(GameObject c in com) {
                Destroy(c);
            }
            StartSpawn();
        }
        if(Time.time - last_spawn >= 8.0f && GameObject.FindGameObjectsWithTag("CommonEnemy").Length <= 3) {
            Spawn();
        }
    }
}
