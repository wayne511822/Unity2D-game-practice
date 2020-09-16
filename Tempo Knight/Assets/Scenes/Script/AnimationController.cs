using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    public GameObject entity;
    private PlayerManager playerManager;

    private SpriteRenderer sr;

    public Sprite Dead;
    private int status, HP, newHP;
    private bool isFall = true;
    private float period = 0.05f;
    private float time;

    void Start() {
        playerManager = entity.GetComponent<PlayerManager>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();

        HP = playerManager.HP;
        newHP = HP;
    }

    private void Update() {
        //取得角色狀態
        status = playerManager.GetStatus();
        //依狀態設置動畫
        anim.SetInteger("status", status);

        

        if (status == 4) { //跌落動畫
            Fall();
        }
        if (HP <= 0) { //死亡時動畫
            EndGame();
        } else {

            GetHit(); //受傷時動畫
            Heal(); //治療時動畫
        }

    }

    private void GetHit() {
        //角色現在血量
        HP = playerManager.HP;
        //如果角色現在血量比記錄的少
        if (newHP > HP) {
            //閃爍效果
            sr.color = new Color(255, 0, 0);
            time += Time.deltaTime;
            if (time >= period) {
                sr.color = new Color(255, 255, 255);
            }
            if (time >= period * 2) {
                sr.color = new Color(255, 0, 0);
            }
            if (time >= period * 3) {
                sr.color = new Color(255, 255, 255);
                time = 0;
                newHP = HP; //記錄的血量重新賦值
            }  

        }
        
    }

    private void Heal() {
        HP = playerManager.HP;
        if (newHP < HP) {
            //閃爍效果
            sr.color = new Color(0, 255, 0);
            time += Time.deltaTime;
            if (time >= period) {
                sr.color = new Color(255, 255, 255);
            }
            if (time >= period * 2) {
                sr.color = new Color(0, 255, 0);
            }
            if (time >= period * 3) {
                sr.color = new Color(255, 255, 255);
                time = 0;
                newHP = HP;
            }
        }
    }

    private void EndGame() {
        //動畫關閉
        anim.enabled = false;
        //切換成死亡的圖片
        sr.sprite = Dead;
        playerManager.canMove = false;

        time += Time.deltaTime;
        if (time >= period * 40) { //2秒後切換
            SceneManager.LoadScene(2);
        }
    }

    private void Fall() {
        //只讓anim.SetTrigger("Fall")執行一次,不要重複呼叫執行
        if (isFall) {
            anim.SetTrigger("Fall");
            isFall = false;
            playerManager.canMove = false;
        }
        
        time += Time.deltaTime;
        if (time >= period * 40) { //2秒後切換
            SceneManager.LoadScene(2);
        }
    }
}
