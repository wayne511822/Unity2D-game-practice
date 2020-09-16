using System;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //角色血量
    public int HP = 6;
    //每次移動多少格
    public float Move_Unity = 1;
    //攻擊範圍
    public GameObject attackArea;
    private GameObject player;

    //紀錄角色狀態
    private readonly int UP = 0;
    private readonly int DOWN = 1;
    private readonly int LEFT = 2;
    private readonly int RIGHT = 3;
    private readonly int FALL = 4;
    private readonly int ATTACK = 400;
    private bool canAttack = false;
    private int status = 1;
    private int saveStatus = 1;

    //紀錄角色現在位置
    private Vector2 nowPosition;
    //紀錄角色將要移動到的位置
    private Vector2 newPosition;
    //是否是在移動狀態
    private bool isMove = false;
    //是否可以移動
    public bool canMove = true; 

    void Start() {
        player = this.gameObject;

        float x = (float) Math.Round(player.transform.position.x, 4);
        float y = (float) Math.Round(player.transform.position.y, 4);
        nowPosition = new Vector2(x, y);
        newPosition = new Vector2(x, y);

    }

    void Update() {

        if (canMove) {
            Movement();
        }
        ComparePosition();

        if (canAttack) {
            Attack();
        }

    }
 
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            newPosition = nowPosition;

        }

        if (collision.gameObject.tag == "Reward") {
            HP++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Boundary") {
            status = FALL;
            saveStatus = FALL;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public int GetStatus() {
        return status;
    }
    public Vector2 GetNowPosition() {
        return nowPosition;
    }

    /*
     * 判斷新舊位置是否相同, 確保完整移動到新位置再做移動判斷
     */
    private void ComparePosition() {
        bool checkX = newPosition.x == Math.Round(player.transform.position.x, 4);
        bool checkY = newPosition.y == Math.Round(player.transform.position.y, 4);
        if (checkX && checkY) {
            nowPosition = newPosition;
            isMove = false;
            status = saveStatus;
        }
    }

    private void Movement() {
        if(status >= 400) {
            status -= ATTACK;
        }

        if (Input.GetKeyDown("w")) { //向上
            status = UP;
            saveStatus = UP;
            if (!isMove) {
                isMove = true;
                canAttack = true;
                newPosition = new Vector2(nowPosition.x, nowPosition.y + Move_Unity);

            } else {
                print("Miss");
            }

        } else if (Input.GetKeyDown("s")) { //向下
            status = DOWN;
            saveStatus = DOWN;
            if (!isMove) {
                isMove = true;
                canAttack = true;
                newPosition = new Vector2(nowPosition.x, nowPosition.y - Move_Unity);

            } else {
                print("Miss");
            }

        } else if (Input.GetKeyDown("a")) { //向左
            status = LEFT;
            saveStatus = LEFT;
            if (!isMove) {
                isMove = true;
                canAttack = true;
                newPosition = new Vector2(nowPosition.x - Move_Unity, nowPosition.y);

            } else {
                print("Miss");
            }

        } else if (Input.GetKeyDown("d")) { //向右
            status = RIGHT;
            saveStatus = RIGHT;
            if (!isMove) {
                isMove = true;
                canAttack = true;
                newPosition = new Vector2(nowPosition.x + Move_Unity, nowPosition.y);

            } else {
                print("Miss");
            }

        }

        if (isMove) {
            player.transform.position = Vector2.Lerp(player.transform.position, newPosition, 0.4f);
        }

    }

    private void Attack() {

        Instantiate(attackArea, newPosition, new Quaternion(0, 0, 0, 0));
        canAttack = false;
    }


}
