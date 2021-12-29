using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private GameObject enemy;

    [SerializeField]
    [Tooltip("’e")]
    private GameObject bullet;

    private GameObject explosion;

    private bool isBomb;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("BombPoint"); 
        explosion = GameObject.Find("enemy");
        isBomb = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireBombs();
        }
    }

    public void FireBombs()
    {
        if (!isBomb) { return; }

        isBomb = false;

        for (int i = 0; i <= 6; i++)
        {
            StartCoroutine(Bombs(i));
        }
    }

    IEnumerator Bombs(int i)
    {
       
            Debug.Log("”­");
            transform.rotation = Quaternion.AngleAxis((360 / 6) * i, new Vector3((i - 3f) / 10.0f, 1, 0));
            Vector3 bulletPosition = enemy.transform.position;
            GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
            Vector3 direction = newBall.transform.up;
            newBall.GetComponent<Rigidbody>().AddForce(direction * 25, ForceMode.Impulse);
            newBall.name = bullet.name;

            yield return new WaitForSeconds(8.0f);

            explosion.GetComponent<explosionPoint>().explosion(newBall.transform.position);
            Destroy(newBall);
        
    }

    private void Explosion()
    {
       
    }

}
