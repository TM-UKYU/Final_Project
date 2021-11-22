using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquipItem : MonoBehaviour
{
    public Image itemListCenter;
    public Image itemListLeft;
    public Image itemListRight;

    public Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        // �A�C�e�����X�g��\��
        itemListLeft.enabled = false;
        itemListRight.enabled = false;
        
        centerPos = itemListCenter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // LCTRL�����������ꂽ��
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // �A�C�e�����X�g�\��
            itemListCenter.enabled = true;
            itemListLeft.enabled = true;
            itemListRight.enabled = true;

            // �ʒu�ϊ��p
            Vector3 itemListFakePos;

            // �}�E�X�z�C�[�����o
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel > 0.0f)
            {
                // �ʒu����ւ�(�E��)
                itemListFakePos = itemListCenter.transform.position;
                itemListCenter.transform.position = itemListRight.transform.position;
                itemListRight.transform.position = itemListLeft.transform.position;
                itemListLeft.transform.position = itemListFakePos;
            }
            else if (mouseScrollWheel < 0.0f)
            {
                // �ʒu����ւ�(����)
                itemListFakePos = itemListCenter.transform.position;
                itemListCenter.transform.position = itemListLeft.transform.position;
                itemListLeft.transform.position = itemListRight.transform.position;
                itemListRight.transform.position = itemListFakePos;
            }
        }
        else
        {
            // �A�C�e�����X�g��\��
            if(centerPos == itemListCenter.transform.position)
            {
                itemListCenter.enabled = true;
                itemListLeft.enabled = false;
                itemListRight.enabled = false;
            }
            else if(centerPos == itemListLeft.transform.position)
            {
                itemListCenter.enabled = false;
                itemListLeft.enabled = true;
                itemListRight.enabled = false;
            }
            else if(centerPos == itemListRight.transform.position)
            {
                itemListCenter.enabled = false;
                itemListLeft.enabled = false;
                itemListRight.enabled = true;
            }
        }
    }
}
