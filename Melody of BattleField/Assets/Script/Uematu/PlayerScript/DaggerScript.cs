using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    void EndEvent()
    {
        Debug.Log("É_ÉKÅ[çÌèú");
        Destroy(gameObject);
    }
}
