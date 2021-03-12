using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {
    // Código retirado do tutorial:
    // https://www.youtube.com/watch?v=3UO-1suMbNc
    public GameObject[] backgrounds;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;


    void Start() {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        foreach(GameObject obj in backgrounds) {
            LoadChilds(obj);
        }
    }

    void LoadChilds(GameObject obj) {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        int childsNeeded = (int) Mathf.Ceil((screenBounds.x * 2) / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++) {
            GameObject myClone = Instantiate(clone) as GameObject;
            myClone.transform.SetParent(obj.transform);
            myClone.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            myClone.name = obj.name + i;
            if (i >= 1) {
                myClone.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObjects(GameObject obj) {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1) {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth*2,
                                                            lastChild.transform.position.y, lastChild.transform.position.z);
                firstChild.transform.Rotate(0.0f, 180.0f, 0.0f);
            } else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth) {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth*2,
                                                           firstChild.transform.position.y, 
                                                           firstChild.transform.position.z);
                lastChild.transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }
    
    void LateUpdate() {
        foreach(GameObject obj in backgrounds) {
            repositionChildObjects(obj);
        }
    }
}
