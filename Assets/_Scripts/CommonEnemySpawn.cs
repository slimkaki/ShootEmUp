using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemySpawn : SteerableBehaviour {
    
    public GameManager gm;

    public GameObject Enemy;

    private float last_spawn = 0.0f;
    void Start() {
        gm = GameManager.GetInstance();
        Instantiate(Enemy, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, transform);
        Instantiate(Enemy, new Vector3(2.0f, 2.0f, 0.0f), Quaternion.identity, transform);
        Instantiate(Enemy, new Vector3(2.0f, -2.0f, 0.0f), Quaternion.identity, transform);
    }

    private void Spawn() {
        float randomY = Random.Range(5.0f, -5.0f);
        float randomX = Random.Range(15.0f, 25.0f);
        Instantiate(Enemy, new Vector3(randomX, randomY, 0.0f), Quaternion.identity, transform);
    }

    private void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - last_spawn >= 8.0f && GameObject.FindGameObjectsWithTag("CommonEnemy").Length <= 3) {
            Spawn();
        }
    }
}
