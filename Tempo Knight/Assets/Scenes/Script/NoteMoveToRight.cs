using UnityEngine;

public class NoteMoveToRight : MonoBehaviour {

    private float speed;
    private float force;
    private bool start = false;
    public float speedFactor = 1.3f; 

    void Update() {

        speed = (BPeerM.GetBPeerM().bpm / 60) * speedFactor;

        if (BPeerM.beatFull) {
            start = true;
        }

        if (start) {
            
            force = speed * Time.deltaTime;
        
            transform.Translate(Vector2.right * force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      
        if (collision.tag == "RightNote") {
            Destroy(this.gameObject, 0.05f);
        }
    }
}
