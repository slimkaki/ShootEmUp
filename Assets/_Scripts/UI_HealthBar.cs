using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour {
    
    // Foi utilizado o seguinte tutorial para esta etapa:
    // https://www.youtube.com/watch?v=BLfNP4Sc_iA
    Slider slider;
    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }
    GameManager gm;
    void Start() {
        slider = GetComponent<Slider>();
        gm = GameManager.GetInstance();
        SetMaxHealth(gm.vidas);
    }
    
    void Update() {
        // if (gm.gameState != GameManager.GameState.GAME) return;
        SetHealth(gm.vidas);
    }
    public void SetHealth(int health) {
        slider.value = health;
    }
}
