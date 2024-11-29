using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    private Vector3 targetPosition; // 目標位置
    public float speed = 5.0f; // 移動速度
    private bool isMoving = false; // 移動中かどうかのフラグ

    void Start() {
        targetPosition = transform.position; // 初期位置を目標位置に設定
    }

    void Update() {
        //transform.position += transform.forward * 1f * Time.deltaTime;
        targetPosition.z = transform.position.z;
        // 移動中でない場合のみキー入力を受け付ける
        if (!isMoving) {
            if (Input.GetKeyDown(KeyCode.W)) {
                targetPosition = new Vector3(transform.position.x, 2.28f, transform.position.z);
                isMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                targetPosition = new Vector3(transform.position.x, -0.3f, transform.position.z);
                isMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                targetPosition = new Vector3(-2.0f, transform.position.y, transform.position.z);
                isMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                targetPosition = new Vector3(2.0f, transform.position.y, transform.position.z);
                isMoving = true;
            }
        }

        // 現在位置から目標位置に向けてLerpで滑らかに移動
        if (isMoving) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            // 目標位置にほぼ到達したら移動完了とみなす
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
                transform.position = targetPosition; // 正確な位置に合わせる
                isMoving = false; // 移動完了
            }
        }
    }
}
