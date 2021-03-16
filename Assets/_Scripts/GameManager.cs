using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    public enum GameState { MENU, GAME, PAUSE, ENDGAME };
    public GameState gameState { get; private set; }
    public int vidas;
    public int pontos;
    private static GameManager _instance;

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
        }
        gameState = nextState;
        changeStateDelegate();
    }
   private GameManager() {
       vidas = 5;
       pontos = 0;
       gameState = GameState.MENU;
   }
}
