using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnHit : MonoBehaviour {
    [Header("Behaviour Setting")]
    public Transform target;

   
    public static bool inHitZone = false;
    private float currentSize;
    public float growSize, shrinkSize;

    [Range(0.8f, 0.99f)]
    public float shrinkFactor;
        
    void Start() {

        if (target == null) {
            target = this.transform;
        }
        currentSize = shrinkSize;
    }

    void Update() {
        if (currentSize > shrinkSize) {
            currentSize *= shrinkFactor;
        } else {
            currentSize = shrinkSize;
        }
        CheckBeat();
        target.localScale = new Vector2(currentSize, currentSize);
        //print(inHitZone);
    }

    void Grow() {
        currentSize = growSize;
    }

    void CheckBeat() {

        if (inHitZone) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) {
                Grow();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "LeftNote" || collision.tag == "RightNote") {
            inHitZone = true;
        }

        if (collision.name == "close") {
            inHitZone = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (inHitZone) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) {
                //Destroy(collision.gameObject);
                inHitZone = false;
            }
        }
    }

}
