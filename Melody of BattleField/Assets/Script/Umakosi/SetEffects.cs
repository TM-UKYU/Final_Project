using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject follow;
    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private bool isUpdate;
    // Start is called before the first frame update
    void Start()
    {
        effect=Instantiate(effect, transform.position, transform.rotation);
        effect.transform.rotation= Quaternion.Euler(0.0f, 90.0f, 0.0f);
        effect.SetActive(false);
    }

    private void Update()
    {
        if (!isUpdate) { return; }
        effect.SetActive(true);

        effect.transform.position = follow.transform.position;
    }
    // Update is called once per frame
    public void EffectUpdate(Vector3 pos)
    {
        effect.SetActive(true);

        effect.transform.position = pos;
    }

    public void EffectEnd()
    {
        effect.SetActive(false);
    }

}
