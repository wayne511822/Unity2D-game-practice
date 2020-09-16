using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Socre : MonoBehaviour {

    private int score;
    private int kill = 0;
    private float period = 0.1f;
    private float time;
    private bool counter;
    private float x, y;

    public GameObject entity;
    public Text text;

    void Start() {
        score = BPeerM.score;
        x = -6;
        y = 1;
    }

    // Update is called once per frame
    void Update() {

        time += Time.deltaTime;
        if (time >= period) {
            time = 0;
            counter = true;
            score--;
        }


        if (counter && score >= 0) {
            kill++;
            Instantiate(entity, new Vector2(x, y), new Quaternion(0, 0, 0, 0));
            x += 0.5f;
            if (x == 6) {
                x = -6;
                y--;
            }
            counter = false;
        }

        text.text = "Kill : " + kill;
    }
}
