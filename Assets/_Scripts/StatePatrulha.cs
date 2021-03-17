using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulha : State {
    
    SteerableBehaviour steerable;
    GameManager gm;
    public override void Awake() {
        base.Awake();
        gm = GameManager.GetInstance();
        //Criamos e populamos uma nova transição
        Transition ToAtacando = new Transition();
        ToAtacando.condition = new ConditionDistLT(transform, GameObject.FindWithTag("Player").transform, 3.0f);
        ToAtacando.target = GetComponent<StateAtaqueAlien>();

        // Adicionamos nossa transição na lista de transições
        transitions.Add(ToAtacando);
        steerable = GetComponent<SteerableBehaviour>();
    }

    float angle = 0.0f;
    public void Update() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        angle += 0.1f * Time.deltaTime;
        Mathf.Clamp(angle, 0.0f, 0.5f * Mathf.PI);
        float x = -1.0f;
        float y = Mathf.Sin(angle);
        if ((transform.position.y > 4.5f && y > 0) || (transform.position.y < -4.9f && y < 0)) {
            y = -y;
        }
        if (Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) > 1f) {
            RotateTowards(GameObject.FindWithTag("Player").transform.position);
        }

        steerable.Thrust(x, y);
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
