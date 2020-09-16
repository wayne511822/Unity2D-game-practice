using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int HP = 1;

    public float Move_Unity = 1;
    public GameObject attackArea;

    private  bool isFall = true;
    private readonly int Hitted = 5;

    public Animator anim;
    private GameObject player;
    private GameObject enemy;
    private PlayerManager playerManager;

    private Vector2 nowPosition;
    private Vector2 newPosition;

    private bool canMove = false;
    private bool canAttack = false;

    void Start() {
        enemy = this.gameObject;
        player = GameObject.Find("Player");
        nowPosition = enemy.transform.position;
        newPosition = nowPosition;
        playerManager = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update() {
        AnimationControl();
        ComparePosition();
        if (GrowOnHit.inHitZone) {
            canMove = true;
        }
        if (canMove) {
            Movement();
        }
        
        if (canAttack) {
            Attack();
        }
    }

    private void AnimationControl() {
        if (HP <= 0) {
            Destroy(this.gameObject, 0.2f);
            anim.SetInteger("status", Hitted);
        }
    }

    private void Movement() {
        //取得角色座標
        Vector2 playerPosition = playerManager.GetNowPosition();
        float playerX = playerPosition.x;
        float playerY = playerPosition.y;

        //取得怪物座標
        float enemyX = nowPosition.x;
        float enemyY = nowPosition.y;

        //取得1 or 2隨機數,用於決定方向
        int r = (int)UnityEngine.Random.Range(1, 3);

        //在節拍上可以移動     
        if (BPeerM.beatFull) {
            
            if (playerX > enemyX && playerY > enemyY) { //角色在第一象限

                if (r == 1) { //為1向上走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y + Move_Unity);
                } else { //否則向右走
                    newPosition = new Vector2(nowPosition.x + Move_Unity, nowPosition.y);
                }

            } else if (playerX < enemyX && playerY > enemyY) { //角色在第二象限

                if (r == 1) { //為1向上走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y + Move_Unity);
                } else { //否則向左走
                    newPosition = new Vector2(nowPosition.x - Move_Unity, nowPosition.y);
                }
             

            } else if (playerX < enemyX && playerY < enemyY) { //角色在第三象限
 
                if (r == 1) { //為1向下走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y - Move_Unity);
                } else { //否則向左走
                    newPosition = new Vector2(nowPosition.x - Move_Unity, nowPosition.y);
                }
                
            } else if (playerX > enemyX && playerY < enemyY) { //角色在第四象限

                if (r == 1) { //為1向下走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y - Move_Unity);
                } else { //否則向右走
                    newPosition = new Vector2(nowPosition.x + Move_Unity, nowPosition.y);
                }
              
            } else if (playerX == enemyX) { //同在X軸上

                float d = playerY - enemyY;
                if (d > 0) { //角色在上方, 往上走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y + Move_Unity);
                } else if(d < 0) { //角色在下方, 往下走
                    newPosition = new Vector2(nowPosition.x, nowPosition.y - Move_Unity);
                }

            } else if (playerY == enemyY) { //同在Y軸上

                float d = playerX - enemyX;
               if (d > 0) { //角色在右邊, 往右走
                    newPosition = new Vector2(nowPosition.x + Move_Unity, nowPosition.y);
                } else if (d < 0) { //角色在左邊, 往左走
                    newPosition = new Vector2(nowPosition.x - Move_Unity, nowPosition.y);
                }
            }
            canAttack = true;
        }

        enemy.transform.position = Vector2.Lerp(enemy.transform.position, newPosition, 0.3f);
       
    }

    /*
     * 判斷新舊位置是否相同, 確保完整移動到新位置再做移動判斷
     */
    private void ComparePosition() {
        bool checkX = newPosition.x == Math.Round(enemy.transform.position.x, 4);
        bool checkY = newPosition.y == Math.Round(enemy.transform.position.y, 4);
        if (checkX && checkY) {
            nowPosition = newPosition;
        }
    }

    private void Attack() {
        Instantiate(attackArea, newPosition, new Quaternion(0, 0, 0, 0));
        canAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //撞到物體反彈
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
            newPosition = nowPosition;
        }
        //撞到Reward摧毀它
        if (collision.gameObject.tag == "Reward") {
            Destroy(collision.gameObject);
        }
        //撞到邊界跌落
        if (collision.gameObject.tag == "Boundary") {
            if (isFall) {
                anim.SetTrigger("Fall");
                isFall = false;
            }
            Destroy(this.gameObject, 2f);
        }

    }

    public Vector2 GetNewPosition() {
        return newPosition;
    }
}
