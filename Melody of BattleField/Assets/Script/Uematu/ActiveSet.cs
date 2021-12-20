using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveSet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //�����ꂽ�Ƃ��ɕ\������I�u�W�F�N�g
    public GameObject MelodyObject;
    //�����ꂽ�Ƃ��Ɍ��ɕ\�����郂���X�^�[�̃I�u�W�F�N�g
    public GameObject MonstarObject;
    private GameObject Instance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�����f�B�̃I�u�W�F�N�g�\��
        MelodyObject.gameObject.SetActive(true);
        MelodyObject.transform.position = this.transform.position;
        MelodyObject.transform.position += new Vector3(30, 0, 0);
        //�����X�^�[�̐���
        Instance = (GameObject)Instantiate(MonstarObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //�����f�B�̃I�u�W�F�N�g��\��
        MelodyObject.gameObject.SetActive(false);

        Destroy(Instance);
    }
}
