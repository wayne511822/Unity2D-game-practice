using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    private GameObject[] enemys;
    public GameObject enemy, player;

    private Vector2 location;

    //Tilemap的外圍座標,敵人出生的位置
    private Vector2[] canCreatePosition = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0), new Vector2(4, 0), new Vector2(5, 0), new Vector2(6, 0), new Vector2(7, 0), new Vector2(8, 0),
                                            new Vector2(0, -8), new Vector2(1, -8), new Vector2(2, -8), new Vector2(3, -8), new Vector2(4, -8), new Vector2(5, -8), new Vector2(6, -8), new Vector2(7, -8), new Vector2(8, -8),
                                            new Vector2(0, -1), new Vector2(0, -2), new Vector2(0, -3), new Vector2(0, -4), new Vector2(0, -5), new Vector2(0, -6), new Vector2(0, -7),
                                            new Vector2(8, -1), new Vector2(8, -2), new Vector2(8, -3), new Vector2(8, -4), new Vector2(8, -5), new Vector2(8, -6), new Vector2(8, -7),
                                            };
    //儲存地圖物件的座標
    private List<Vector2> entityPosition;

    //敵人出生速度
    public float createSpeed = 4;
    //敵人生成時間
    private float createTime;
    //紀錄遊戲時間
    private float gameTime;
    //難度提升的秒數
    private int period = 12;

    void Update() {

        //紀錄遊戲時間
        gameTime += Time.deltaTime;
        //敵人生成時間
        createTime += Time.deltaTime;

        //一定時間後減少敵人生成間隔
        if (createSpeed > 1 && (int)(gameTime / period) == 1) {
            createSpeed--;
            period += 12;
            print("createSpeed" + createSpeed);
        }

        //一定時間後生成敵人
        if (createTime >= createSpeed) {
            location = GetVector2();
            createTime = 0;
            Instantiate(enemy, location, new Quaternion(0, 0, 0, 0));
        }

    }
    
    private Vector2 GetVector2() {
        //清空List
        entityPosition = new List<Vector2>();

        int len = canCreatePosition.Length;

        //將地圖上敵人位置存入Lsit中
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++) {
            Vector2 ep = enemys[i].GetComponent<EnemyManager>().GetNewPosition();
            entityPosition.Add(ep);
        }
        //將角色位置存入Lsit中
        Vector2 pp = player.GetComponent<PlayerManager>().GetNowPosition();
        entityPosition.Add(pp);

        //取得隨機index
        int index = (int)UnityEngine.Random.Range(0, len);
        while (true) {
            //重新取隨機數
            index = (int)UnityEngine.Random.Range(0, len);

            if (!entityPosition.Contains(canCreatePosition[index])) {
                return canCreatePosition[index];
            }
        }

    }


}
