using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    private Vector3 targetPosition; // 目標位置
    public float speed = 5.0f; // 移動速度
    private bool isMoving = false; // 移動中かどうかのフラグ
    private int passenger = 0;
    private bool hitStation;
    private int subTrainNum;
    private int saveSubTrainNum;  //車両が増えた瞬間を取る変数

    [SerializeField] private List<GameObject> subTrains = new List<GameObject>();
    [SerializeField] private MoveCamera moveCamera;
    void Start() {
        targetPosition = transform.position; // 初期位置を目標位置に設定
        passenger = 0;
        hitStation = false;
        subTrainNum = 0;
        saveSubTrainNum = subTrainNum;
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

        //Passengerが5000を超えないように
        if (passenger >= 5000) {
            passenger = 5000;
        }
        //Passengerが0を下回らないように
        if (passenger <= 0) {
            passenger = 0;
        }
        //SubTrainが0を下回らないように
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

    //HitStationのゲッターセッター
    public bool GetHitStation() {
        return hitStation;
    }
    public void SetHitStation(bool flag) {
        hitStation = flag;
    }

    //外部でPassengerを足す時の関数
    public void AddPassenger() {
        passenger += 500;
    }

    //Passengerのゲッターセッター
    public int GetPassenger() {
        return passenger;
    }
    public void SetPassenger(int num) {
        passenger = num;
    }

    //SubTrainsNumの値を減らす関数
    public void SubtractSubTrain() {
        passenger -= 500;
        subTrainNum--;
        moveCamera.InCamara();
    }

    //SubTrainNumのゲッター
    public int GetSubTrainNum() {
        return subTrainNum;
    }
}
