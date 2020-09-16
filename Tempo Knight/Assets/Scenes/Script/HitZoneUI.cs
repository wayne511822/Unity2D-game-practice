using UnityEngine;

public class HitZoneUI : MonoBehaviour {

    public GameObject leftNote;
    public GameObject rightNote;
    public GameObject hitZone;
    public GameObject Left;
    public GameObject Right;

    float creatSpeed;
    float beatTimer = 0;
    bool create = false;

    private float bpm;

    void Start() {

        bpm = BPeerM.GetBPeerM().bpm;
        creatSpeed = 60 / bpm;
    }

    // Update is called once per frame
    void Update() {

        beatTimer += Time.deltaTime;

        if (beatTimer >= creatSpeed) {
            beatTimer -= creatSpeed;
            create = true;

            if (create) {

                Instantiate(leftNote, new Vector2(Left.transform.position.x, hitZone.transform.position.y), new Quaternion(0,0,0,0));
                Instantiate(rightNote, new Vector2(Right.transform.position.x, hitZone.transform.position.y), new Quaternion(0,0,0,0));
                create = false;
                //print("create");
            }
        }

    }
}
