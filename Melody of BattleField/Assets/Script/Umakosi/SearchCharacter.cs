using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Collider))]
public class SearchCharacter : MonoBehaviour
{

    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerExit = new TriggerEvent();

    /// <summary>
    /// Is Trigger��ON�ő���Collider�Əd�Ȃ��Ă���Ƃ��ɌĂ΂ꑱ����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // Inspector�^�u��onTriggerStay�Ŏw�肳�ꂽ���������s����
        onTriggerStay.Invoke(other);
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke(other);   
    }

    // UnityEvent���p�������N���X��[Serializable]������t�^���邱�ƂŁAInspector�E�C���h�E��ɕ\���ł���悤�ɂȂ�B
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }

}
