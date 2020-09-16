using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

    public GameObject player;
    public Sprite Full, Half, Empty;
    
    private Image[] heartImage;
    private int playerHP;

    private float period = 3f;
    private float time;

    void Start() {
        
        heartImage = new Image[3];
        playerHP = player.GetComponent<PlayerManager>().HP;

        for (int i = 0; i < heartImage.Length; i++) {
            heartImage[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update() {
        playerHP = player.GetComponent<PlayerManager>().HP;
        switch (playerHP) {
            case 6:
                showHP(Full, Full, Full);
                break;
            case 5:
                showHP(Full, Full, Half);
                break;
            case 4:
                showHP(Full, Full, Empty);
                break;
            case 3:
                showHP(Full, Half, Empty);
                break;
            case 2:
                showHP(Full, Empty, Empty);
                break;
            case 1:
                showHP(Half, Empty, Empty);
                break;
            case 0:
                showHP(Empty, Empty, Empty);
                break;
        }
    }

    private void showHP(Sprite sp1, Sprite sp2, Sprite sp3) {
        Sprite[] sprites = {sp1, sp2, sp3};

        for (int i = 0; i < heartImage.Length; i++) {
            heartImage[i].sprite = sprites[i];
        }
    }

    
}
