using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    private Vector3 targetPosition; // �ڕW�ʒu
    public float speed = 5.0f; // �ړ����x
    private bool isMoving = false; // �ړ������ǂ����̃t���O

    void Start() {
        targetPosition = transform.position; // �����ʒu��ڕW�ʒu�ɐݒ�
    }

    void Update() {
        //transform.position += transform.forward * 1f * Time.deltaTime;
        targetPosition.z = transform.position.z;
        // �ړ����łȂ��ꍇ�̂݃L�[���͂��󂯕t����
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

        // ���݈ʒu����ڕW�ʒu�Ɍ�����Lerp�Ŋ��炩�Ɉړ�
        if (isMoving) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

            // �ڕW�ʒu�ɂقړ��B������ړ������Ƃ݂Ȃ�
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
                transform.position = targetPosition; // ���m�Ȉʒu�ɍ��킹��
                isMoving = false; // �ړ�����
            }
        }
    }
}
