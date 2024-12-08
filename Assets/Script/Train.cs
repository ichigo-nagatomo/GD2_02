using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    private Vector3 targetPosition; // �ڕW�ʒu
    public float speed = 5.0f; // �ړ����x
    private bool isMoving = false; // �ړ������ǂ����̃t���O
    private int passenger = 0;
    private bool hitStation;
    private int subTrainNum;
    private int saveSubTrainNum;  //�ԗ����������u�Ԃ����ϐ�

    [SerializeField] private List<GameObject> subTrains = new List<GameObject>();
    [SerializeField] private MoveCamera moveCamera;
    void Start() {
        targetPosition = transform.position; // �����ʒu��ڕW�ʒu�ɐݒ�
        passenger = 0;
        hitStation = false;
        subTrainNum = 0;
        saveSubTrainNum = subTrainNum;
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

        //Passenger��5000�𒴂��Ȃ��悤��
        if (passenger >= 5000) {
            passenger = 5000;
        }
        //Passenger��0�������Ȃ��悤��
        if (passenger <= 0) {
            passenger = 0;
        }
        //SubTrain��0�������Ȃ��悤��
        if (subTrainNum <= 0) {
            subTrainNum = 0;
        }

        SubTrainMana();
        if (saveSubTrainNum < subTrainNum) {
            moveCamera.OutCamara();
        }
        saveSubTrainNum = subTrainNum;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Station") {
            hitStation = true;
        }
    }

    private void SubTrainMana() {
        if (passenger >= 1001) {
            subTrainNum = 1;
        }
        if (passenger >= 2001) {
            subTrainNum = 2;
        }
        if (passenger >= 3001) {
            subTrainNum = 3;
        }
        if (passenger >= 4001) {
            subTrainNum = 4;
        }

        if (subTrainNum >= 1) {
            subTrains[0].SetActive(true);
        } else {
            subTrains[0].SetActive(false);
        }
        if (subTrainNum >= 2) {
            subTrains[1].SetActive(true);
        } else {
            subTrains[1].SetActive(false);
        }
        if (subTrainNum >= 3) {
            subTrains[2].SetActive(true);
        } else {
            subTrains[2].SetActive(false);
        }
        if (subTrainNum >= 4) {
            subTrains[3].SetActive(true);
        } else {
            subTrains[3].SetActive(false);
        }
    }

    //HitStation�̃Q�b�^�[�Z�b�^�[
    public bool GetHitStation() {
        return hitStation;
    }
    public void SetHitStation(bool flag) {
        hitStation = flag;
    }

    //�O����Passenger�𑫂����̊֐�
    public void AddPassenger() {
        passenger += 500;
    }

    //Passenger�̃Q�b�^�[�Z�b�^�[
    public int GetPassenger() {
        return passenger;
    }
    public void SetPassenger(int num) {
        passenger = num;
    }

    //SubTrainsNum�̒l�����炷�֐�
    public void SubtractSubTrain() {
        passenger -= 500;
        subTrainNum--;
        moveCamera.InCamara();
    }

    //SubTrainNum�̃Q�b�^�[
    public int GetSubTrainNum() {
        return subTrainNum;
    }
}
