using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ImageExt;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject lockOnTarget;
    public LockOnTargetDetector lockOnTargetDetector;
    public float rotate_speed;
    private bool clickFlg;

    [SerializeField]
    private Image image ;
    private float toumei;
    private bool lockTargetFlg;

    void Start()
    {

        toumei = 0;
        image.SetOpacity(toumei);

        clickFlg = false;
        lockTargetFlg = true;

        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        lockOnTargetDetector=player.GetComponentInChildren<LockOnTargetDetector>();
    }

    void Update()
    {
        transform.position = player.transform.position;

        if (lockTargetFlg)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                lockTargetFlg = false;
                clickFlg = !clickFlg;
                GameObject target = lockOnTargetDetector.getTarget();
                if (target != null && clickFlg)
                {
                    toumei = 0;
                    StartCoroutine("ScaleDown");
                    lockOnTarget = target;
                }
                else
                {
                    StartCoroutine("ScaleUp");
                    lockOnTarget = null;
                }
            }
        }


        if (lockOnTarget)
        {
            lockOnTargetObject(lockOnTarget);
        }
        else
        {
            rotateCmaeraAngle();
        }

        Debug.Log(toumei);
    }

    IEnumerator ScaleDown()
    {

        for (float i = 7; i > 0.3f; i -= 0.1f)
        {
            toumei += 0.01f;
            image.SetOpacity(toumei);
            image.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }

        lockTargetFlg = true;

    }

    IEnumerator ScaleUp()
    {
        if (toumei < 1.0f) { yield return false; }
        for (float i = 0.3f; i < 7; i += 0.1f)
        {
            toumei -= 0.01f;
            image.SetOpacity(toumei);

            image.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }
        lockTargetFlg = true;
    }

    private void lockOnTargetObject(GameObject target)
    {
        transform.LookAt(target.transform, Vector3.up);
    }

    private void rotateCmaeraAngle()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotate_speed,
            Input.GetAxis("Mouse Y") * rotate_speed,
            0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
}