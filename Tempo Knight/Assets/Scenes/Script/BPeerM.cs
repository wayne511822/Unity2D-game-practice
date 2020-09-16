using UnityEngine;

public class BPeerM : MonoBehaviour {

    //單例對象
    private static BPeerM bPeerMInstance;
    //音樂節拍
    public float bpm;
    private float beatInterval, beatTimer;
    //每到節拍點為true
    public static bool beatFull;
    //遊戲音樂
    public AudioSource music;

    //紀錄敵人擊殺數
    public static int score;

    private void Awake() {
        //確保該對象在程序中只會存在一個
        if (bPeerMInstance != null && bPeerMInstance != this) {
            Destroy(this.gameObject);
        } else {
            bPeerMInstance = this;
            music.Play();
            DontDestroyOnLoad(this.gameObject);

        }
    }

    void Update() {
        BeatDetection();
    }

    
    private void BeatDetection() {
        //full beat count
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval) {
            beatTimer -= beatInterval;
            beatFull = true;
            //print("beatFull : " + beatFull);
        }
    }

    public static BPeerM GetBPeerM() {
        return bPeerMInstance;
    }
}
