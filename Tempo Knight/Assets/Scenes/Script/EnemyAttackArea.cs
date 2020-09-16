using UnityEngine;

public class EnemyAttackArea : MonoBehaviour {

    private Animator playerAnimator;
    private GameObject player;
    

    private void Start() {
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponentInChildren<AnimationController>().anim;
        
    }
    void Update() {
        Destroy(this.gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {
            collision.GetComponent<PlayerManager>().HP -= 1;

        }


        Destroy(this.gameObject);
    }

}
