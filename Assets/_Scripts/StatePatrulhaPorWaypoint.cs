using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulhaPorWaypoint : State {
    
    public Transform[] waypoints;
    SteerableBehaviour steerable;

    public float distance;

    GameManager gm;
    
    public override void Awake() {
        base.Awake();
        gm = GameManager.GetInstance();
        //Criamos e populamos uma nova transição
        Transition ToAtacando = new Transition();
        ToAtacando.condition = new ConditionDistLT(transform, GameObject.FindWithTag("Player").transform, 4.0f);
        ToAtacando.target = GetComponent<StateAtaque>();

        // Adicionamos nossa transição na lista de transições
        transitions.Add(ToAtacando);
        steerable = GetComponent<SteerableBehaviour>();
    }

    public void Start() {
        waypoints[0].position = transform.position;
        waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
    }

    public override void Update() {
        if (gm.gameState != GameManager.GameState.GAME) return; 
        if (Vector3.Distance(transform.position, waypoints[1].position) > distance) {
            Vector3 direction = waypoints[1].position - transform.position;
            direction.Normalize();
            steerable.Thrust(direction.x, direction.y);
        } else {
            waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
        }

    }
}
