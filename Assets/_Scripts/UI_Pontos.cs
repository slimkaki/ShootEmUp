﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pontos : MonoBehaviour {
    Text textComp;
    GameManager gm;
    void Start() {
        textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        textComp.text = $"Points: {gm.pontos}";
    }
}
