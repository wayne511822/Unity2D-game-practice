using System.Diagnostics.Tracing;
using UnityEngine;

public class NoteMoveToLeft : MonoBehaviour {

    private float speed;
    private float force;
    private bool start = false;
    public float speedFactor = 1.3f;

    public bool counter = false;
    private float time, period = 0.5f;
    

    void Update() {

        speed = (BPeerM.GetBPeerM().bpm / 60) * speedFactor;

        if (BPeerM.beatFull) {
            start = true;
        }

        if (start) {

            force = speed * Time.deltaTime;

            transform.Translate(Vector2.left * force);
        }

        if (counter) {
            PerfectHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "LeftNote") {
            Destroy(this.gameObject, 0.05f);
            counter = true;
        }
    }

    private void PerfectHit() {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) {
            print("Prefect!!!");
        }

        if (time >= period) {
            counter = false;
        }
    } 
}
