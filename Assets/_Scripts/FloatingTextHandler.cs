using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandler : MonoBehaviour {

    // Foi utilizado o seguinte tutorial para os pontos na tela:
    // https://www.youtube.com/watch?v=esnaEYYD1ZM 
    void Start() {
        Destroy(gameObject, 1f);
        transform.localPosition += new Vector3(0, 0.5f, 0);
    }
}
