using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    public enum GameState { MENU, GAME, PAUSE, ENDGAME };
    public GameState gameState { get; private set; }
    public int vidas;
    public int pontos;
    public float timeLimit = 120.0f;
    public float time;
    private static GameManager _instance;
    public bool ResetFlag = false;
    public bool backFromPause = false;
    public float pauseTime = 0.0f;
    public float tempoRestante = 120.0f;
    public bool win = false;

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }

       return _instance;
   }

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    public void ChangeState(GameState nextState) {
        if ((gameState == GameState.PAUSE && nextState == GameState.MENU) || 
            (gameState == GameState.ENDGAME && nextState == GameState.GAME)) {
            vidas = 10;
            pontos = 0;
            GameObject.FindWithTag("Player").transform.position = new Vector3(-3.78f, 0.04f, 0.0f);
            time = 0.0f;
            pauseTime = 0.0f;
            ResetFlag = true;
        } else if (gameState == GameState.PAUSE && nextState == GameState.GAME) {
            pauseTime = Time.time - pauseTime;
            backFromPause = true;
        } else if (gameState == GameState.GAME && nextState == GameState.PAUSE) {
            pauseTime = Time.time;

        }
        gameState = nextState;
        changeStateDelegate();
    }
   private GameManager() {
       time = 0.0f;
       vidas = 10;
       pontos = 0;
       gameState = GameState.MENU;
   }
}
