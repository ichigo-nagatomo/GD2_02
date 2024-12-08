using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTrain : MonoBehaviour {
    [SerializeField] private GameObject forwardTrain; // �O���̓d��
    [SerializeField] private float followSpeed = 5f; // �Ǐ]���x

    // Update is called once per frame
    void Update() {
        if (forwardTrain != null) {
            // �O���d�Ԃ̈ʒu���擾
            Vector3 targetPosition = forwardTrain.transform.position;

            // ���g�̍��� (Y) ���ێ����A�x��鋗�����l�������ʒu���v�Z
            Vector3 followPosition = new Vector3(
                targetPosition.x,
                targetPosition.y,
                targetPosition.z - 2.2f
            );

            // ���݂̈ʒu����Ǐ]�ʒu�փX���[�Y�Ɉړ�
            transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);
        }
    }
}
