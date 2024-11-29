using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrain : MonoBehaviour {
    private bool rightHit;
    private bool leftHit;
    private bool upHit;
    private bool downHit;

    private Vector3 moveVelo;
    private Vector3 rote;

    private float destroyTime;
    private float selfDestroyTime;
    void Start() {
        rightHit = false;
        leftHit = false;
        upHit = false;
        downHit = false;
        moveVelo = Vector3.zero;
        rote = Vector3.zero;
        destroyTime = 5f;
        selfDestroyTime = 15f;
    }

    // Update is called once per frame
    void Update() {
        if (!rightHit && !leftHit && !upHit && !downHit) {
            transform.position += transform.forward * 3f * Time.deltaTime;
        }

        if (rightHit || leftHit || upHit || downHit) {
            Fling();
            destroyTime -= Time.deltaTime;
        }

        if (upHit) { //上から当たった
            transform.localScale -= new Vector3(0, Time.deltaTime, 0);

            destroyTime -= 5f * Time.deltaTime;
        }

        if (destroyTime <= 0) {
            Destroy(gameObject);
        }

        selfDestroyTime -= Time.deltaTime;
        if (selfDestroyTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // 衝突したオブジェクトの位置
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // 衝突方向を計算
            Vector3 directionToCollision = (collisionPoint - transform.position).normalized;

            // 基準方向
            Vector3 forward = transform.forward; // 正面
            Vector3 up = transform.up;           // 上
            Vector3 right = transform.right;     // 右

            // 衝突方向を正面、上下、左右で判定
            float dotForward = Vector3.Dot(forward, directionToCollision);
            float dotUp = Vector3.Dot(up, directionToCollision);
            float dotRight = Vector3.Dot(right, directionToCollision);

            if (dotForward > 0.8f) { // 正面
                Debug.Log("正面から衝突！");
            } else if (dotUp > 0.7f) { // 上
                Debug.Log("上から衝突！");
                upHit = true;
            } else if (dotUp < -0.7f) { // 下
                Debug.Log("下から衝突！");
                moveVelo = new Vector3(0, 10, 10);
                rote = new Vector3(-720, 0, 0);
                downHit = true;
            } else if (dotRight > 0.7f) { // 右
                Debug.Log("右から衝突！");
                moveVelo = new Vector3(10, 0, 10);
                rote = new Vector3(0, -720, 0);
                rightHit = true;
            } else if (dotRight < -0.7f) { // 左
                Debug.Log("左から衝突！");
                moveVelo = new Vector3(-10, 0, 10);
                rote = new Vector3(0, 720, 0);
                leftHit = true;
            } else {
                Debug.Log("判定外の方向から衝突！");
            }
        }

        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Fling() {
        transform.Translate(moveVelo.x * Time.deltaTime, moveVelo.y * Time.deltaTime, moveVelo.z * Time.deltaTime, Space.World);
        transform.Rotate(rote.x * Time.deltaTime, rote.y * Time.deltaTime, rote.z * Time.deltaTime);

        //画面の端に行ったら
        if (transform.position.x <= -15 || transform.position.x >= 15) {
            moveVelo.x *= -1f;
        }

        //上に飛んでいたら
        if (transform.position.y > 2.28f) {
            moveVelo.y -= 10f * Time.deltaTime;
        }

        //地面に着いたら
        if (transform.position.y < -0.3f) {
            moveVelo.y = 10f;
        }
    }
}

