using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    [SerializeField] Vector3 moveVec = new Vector3(1, 0, -1); // 移動する方向と距離
    private Vector3 targetPosition;
    private bool isMoving = false; // 移動中かどうかのフラグ
    public float speed = 5.0f; // 移動速度
    void Start() {
        targetPosition = transform.position;
    }
    void Update() {
        if (!isMoving) {
            // Qキーが押された場合に前方向へ移動
            if (Input.GetKeyDown(KeyCode.Q)) {
                targetPosition = transform.position + moveVec;
                isMoving = true;
            }

            // Eキーが押された場合に後方向へ移動
            if (Input.GetKeyDown(KeyCode.E)) {
                targetPosition = transform.position - moveVec;
                isMoving = true;
            }
        }

        if (isMoving) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            // 目標位置にほぼ到達したら移動完了とみなす
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
                transform.position = targetPosition; // 正確な位置に合わせる
                isMoving = false; // 移動完了
            }
        }
    }

    public void OutCamara() {
        targetPosition = transform.position + moveVec;
        isMoving = true;
    }

    public void InCamara() {
        targetPosition = transform.position - moveVec;
        isMoving = true;
    }
}
