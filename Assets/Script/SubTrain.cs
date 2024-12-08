using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTrain : MonoBehaviour {
    [SerializeField] private GameObject forwardTrain; // 前方の電車
    [SerializeField] private float followSpeed = 5f; // 追従速度

    // Update is called once per frame
    void Update() {
        if (forwardTrain != null) {
            // 前方電車の位置を取得
            Vector3 targetPosition = forwardTrain.transform.position;

            // 自身の高さ (Y) を維持し、遅れる距離を考慮した位置を計算
            Vector3 followPosition = new Vector3(
                targetPosition.x,
                targetPosition.y,
                targetPosition.z - 2.2f
            );

            // 現在の位置から追従位置へスムーズに移動
            transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);
        }
    }
}
