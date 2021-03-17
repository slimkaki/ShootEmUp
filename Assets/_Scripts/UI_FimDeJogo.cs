using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FimDeJogo : MonoBehaviour {
    
    public Text message;

    GameManager gm;
    private void OnEnable() {
        gm = GameManager.GetInstance();

        if(gm.win) {
            message.text = $"Congratulations! You made it!\nTotal Points: {gm.pontos}";
        } else {
            if (gm.vidas <= 0) {
                message.text = $"You died!\nTotal Points: {gm.pontos}";
            } else {
                message.text = $"You lost!\nTotal Points: {gm.pontos}";
            }
            
        }
    }

    public void Voltar() {
        gm.ChangeState(GameManager.GameState.GAME);
    }


}
