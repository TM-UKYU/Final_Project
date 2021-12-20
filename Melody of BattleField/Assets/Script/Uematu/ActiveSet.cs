using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveSet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //押されたときに表示するオブジェクト
    public GameObject MelodyObject;
    //押されたときに後ろに表示するモンスターのオブジェクト
    public GameObject MonstarObject;
    private GameObject Instance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //メロディのオブジェクト表示
        MelodyObject.gameObject.SetActive(true);
        MelodyObject.transform.position = this.transform.position;
        MelodyObject.transform.position += new Vector3(30, 0, 0);
        //モンスターの生成
        Instance = (GameObject)Instantiate(MonstarObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //メロディのオブジェクト非表示
        MelodyObject.gameObject.SetActive(false);

        Destroy(Instance);
    }
}
