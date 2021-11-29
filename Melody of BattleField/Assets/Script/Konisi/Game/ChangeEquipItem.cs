using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquipItem : MonoBehaviour
{
    public List<Image> itemList;

    public int centerArrayNum;

    // Start is called before the first frame update
    void Start()
    {
        // �A�C�e���X���C�_�[�̐^�񒆈ȊO��\��
        for(int i = 0; i < itemList.Count; i++)
        {
            if(i != itemList.Count / 2)
            {
                itemList[i].gameObject.SetActive(false);
            }
        }

        centerArrayNum = itemList.Count / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // LCTRL�����������ꂽ��
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // �A�C�e�����X�g�̐^�񒆂ƍ��E�ȊO�\��
            for (int i = 0; i < itemList.Count; i++)
            {
                if(centerArrayNum == 0)
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + 1 ||
                        i == centerArrayNum - itemList.Count - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
                else if(centerArrayNum == itemList.Count)
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + itemList.Count - 1||
                        i == centerArrayNum - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + 1 ||
                        i == centerArrayNum - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
            }

            // �ʒu�ϊ��p
            Vector3 itemListFakePos;

            // �}�E�X�z�C�[�����o
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel > 0.0f)
            {
                if (centerArrayNum != 0)
                {
                    // ���[�L��
                    itemListFakePos = itemList[0].transform.position;
                    // �ʒu����ւ�(�E��)
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i != (itemList.Count - 1))
                        {
                            itemList[i].transform.position = itemList[i + 1].transform.position;
                        }
                    }
                    centerArrayNum--;

                    // �L���������[���E�[��
                    itemList[itemList.Count - 1].transform.position = itemListFakePos;
                }
            }
            else if (mouseScrollWheel < 0.0f)
            {
                if (centerArrayNum != itemList.Count - 1)
                {
                    // �E�[�L��
                    itemListFakePos = itemList[itemList.Count - 1].transform.position;
                    // �ʒu����ւ�(����)
                    for (int i = itemList.Count - 1; i > 0; i--)
                    {
                        itemList[i].transform.position = itemList[i - 1].transform.position;
                    }
                    centerArrayNum++;

                    // �L�������E�[�����[��
                    itemList[0].transform.position = itemListFakePos;
                }
            }
        }
        else
        {
            // �A�C�e�����X�g��\��
            for (int i = 0; i < itemList.Count; i++)
            {
                if(i == centerArrayNum)
                {
                    itemList[i].gameObject.SetActive(true);
                }
                else
                {
                    itemList[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
