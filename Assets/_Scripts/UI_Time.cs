using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour {

    Text textComp;
    GameManager gm;
    float timeBegin;
    void Start() {
        textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
        timeBegin = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (gm.tempoRestante <= 0.01f && gm.vidas > 0) {
            gm.win = true;
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
        if (gm.ResetFlag) {
            timeBegin = Time.time;
            return;
        }
        if (gm.backFromPause) {

            // timeBegin = timeBegin + Time.time;
            gm.backFromPause = false;
            return;
        }
        if (Time.time - gm.time >= 0.1f) {
            gm.time = Time.time - timeBegin;
        }
        gm.tempoRestante = gm.timeLimit - gm.time + gm.pauseTime;
        textComp.text = $"Time Left: {gm.tempoRestante.ToString("F1")} s";
    }
}
