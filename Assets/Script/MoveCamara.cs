using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    [SerializeField] Vector3 moveVec = new Vector3(1, 0, -1); // �ړ���������Ƌ���
    private Vector3 targetPosition;
    private bool isMoving = false; // �ړ������ǂ����̃t���O
    public float speed = 5.0f; // �ړ����x
    void Start() {
        targetPosition = transform.position;
    }
    void Update() {
        if (!isMoving) {
            // Q�L�[�������ꂽ�ꍇ�ɑO�����ֈړ�
            if (Input.GetKeyDown(KeyCode.Q)) {
                targetPosition = transform.position + moveVec;
                isMoving = true;
            }

            // E�L�[�������ꂽ�ꍇ�Ɍ�����ֈړ�
            if (Input.GetKeyDown(KeyCode.E)) {
                targetPosition = transform.position - moveVec;
                isMoving = true;
            }
        }

        if (isMoving) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            // �ڕW�ʒu�ɂقړ��B������ړ������Ƃ݂Ȃ�
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
                transform.position = targetPosition; // ���m�Ȉʒu�ɍ��킹��
                isMoving = false; // �ړ�����
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
