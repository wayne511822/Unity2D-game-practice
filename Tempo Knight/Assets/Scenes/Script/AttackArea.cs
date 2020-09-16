using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AttackArea : MonoBehaviour {

    private Animator playerAnimator;
    public GameObject Reward;
    private float[] px = { 0, 1, 2, 3, 4, 5, 6, 7 ,8 };
    private float[] py = { -8, -7, -6, -5, -4, -3, -2, -1, 0};
    private bool create = false;

    private void Start() {
        playerAnimator = GameObject.Find("Player").GetComponentInChildren<AnimationController>().anim;
    }

    void Update() {
        Destroy(this.gameObject, 0.05f);

        if (create) {
            CreateReward();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Enemy") {
            playerAnimator.SetTrigger("Attack");
            collision.GetComponent<EnemyManager>().HP -= 1;
            BPeerM.score++;
            create = true;
            CreateReward();
            }

        Destroy(this.gameObject);
    }


    private void CreateReward() {
        int r = (int)UnityEngine.Random.Range(0, 4);

        if (r == 1) {
            int x = (int)UnityEngine.Random.Range(0, px.Length);
            int y = (int)UnityEngine.Random.Range(0, py.Length);
            print(px[x] + "," + py[y]);
            Instantiate(Reward, new Vector2(px[x], py[y]), new Quaternion(0, 0, 0, 0));
        }
        
        create = false;
    }
}
