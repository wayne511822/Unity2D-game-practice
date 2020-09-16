using UnityEngine;

public class GrowOnBeat : MonoBehaviour {

    [Header("Behaviour Setting")]
    public Transform target;

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
    }

    void Grow() {
        currentSize = growSize;
    }

    void CheckBeat() {

        if (BPeerM.beatFull) {
            Grow();
        }
    }
}
