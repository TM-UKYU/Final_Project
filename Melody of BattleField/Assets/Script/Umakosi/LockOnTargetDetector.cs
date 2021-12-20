using UnityEngine;

public class LockOnTargetDetector : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    protected void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyCenter");
            target = c.gameObject;
        }
    }

    protected void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyCenter");
            target = null;
        }
    }

    public GameObject getTarget()
    {
        return this.target;
    }
}
