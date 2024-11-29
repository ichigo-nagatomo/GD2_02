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

        if (upHit) { //�ォ�瓖������
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
            // �Փ˂����I�u�W�F�N�g�̈ʒu
            Vector3 collisionPoint = other.ClosestPoint(transform.position);

            // �Փ˕������v�Z
            Vector3 directionToCollision = (collisionPoint - transform.position).normalized;

            // �����
            Vector3 forward = transform.forward; // ����
            Vector3 up = transform.up;           // ��
            Vector3 right = transform.right;     // �E

            // �Փ˕����𐳖ʁA�㉺�A���E�Ŕ���
            float dotForward = Vector3.Dot(forward, directionToCollision);
            float dotUp = Vector3.Dot(up, directionToCollision);
            float dotRight = Vector3.Dot(right, directionToCollision);

            if (dotForward > 0.8f) { // ����
                Debug.Log("���ʂ���ՓˁI");
            } else if (dotUp > 0.7f) { // ��
                Debug.Log("�ォ��ՓˁI");
                upHit = true;
            } else if (dotUp < -0.7f) { // ��
                Debug.Log("������ՓˁI");
                moveVelo = new Vector3(0, 10, 10);
                rote = new Vector3(-720, 0, 0);
                downHit = true;
            } else if (dotRight > 0.7f) { // �E
                Debug.Log("�E����ՓˁI");
                moveVelo = new Vector3(10, 0, 10);
                rote = new Vector3(0, -720, 0);
                rightHit = true;
            } else if (dotRight < -0.7f) { // ��
                Debug.Log("������ՓˁI");
                moveVelo = new Vector3(-10, 0, 10);
                rote = new Vector3(0, 720, 0);
                leftHit = true;
            } else {
                Debug.Log("����O�̕�������ՓˁI");
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

        //��ʂ̒[�ɍs������
        if (transform.position.x <= -15 || transform.position.x >= 15) {
            moveVelo.x *= -1f;
        }

        //��ɔ��ł�����
        if (transform.position.y > 2.28f) {
            moveVelo.y -= 10f * Time.deltaTime;
        }

        //�n�ʂɒ�������
        if (transform.position.y < -0.3f) {
            moveVelo.y = 10f;
        }
    }
}

