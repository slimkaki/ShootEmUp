using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAtaqueAlien : State {
    SteerableBehaviour steerable;
    IShooter shooter;
    GameManager gm;

    public override void Awake() {
        base.Awake();
        gm = GameManager.GetInstance();

        Transition ToPatrulha = new Transition();
        ToPatrulha.condition = new ConditionDistGT(transform, GameObject.FindWithTag("Player").transform, 3.0f);
        ToPatrulha.target = GetComponent<StatePatrulha>();

        // Adicionamos a transição em nossa lista de transições
        transitions.Add(ToPatrulha);

        steerable = GetComponent<SteerableBehaviour>();
        shooter = steerable as IShooter;
        if (shooter == null) {
            throw new MissingComponentException("Este GameObject não implementa IShooter");
        }
    }

    public float shootDelay = 1.0f;
    private float _lastShootTimestamp = 0.0f;
    public override void Update() {
        if (gm.gameState != GameManager.GameState.GAME) return; 
        
        Vector2 direction = new Vector2(GameObject.FindWithTag("Player").transform.position.x - transform.position.x, GameObject.FindWithTag("Player").transform.position.y - transform.position.y);
        direction.Normalize();

        steerable.Thrust(direction.x, direction.y);

        if (Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) > 1f) {
            RotateTowards(GameObject.FindWithTag("Player").transform.position);
        }

        if (Time.time - _lastShootTimestamp < shootDelay) return;
        _lastShootTimestamp = Time.time;
        shooter.Shoot();
    }

    // Função retirada de uma questão presente no fórum da unity 
    // https://answers.unity.com/questions/1592029/how-do-you-make-enemies-rotate-to-your-position-in.html
    private void RotateTowards(Vector2 target) {
        var offset = 0f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
