using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float beforeDestroy = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        beforeDestroy -= Time.deltaTime;
        BulletTimeCheck();
    }

    void BulletTimeCheck()
    {
        if (beforeDestroy < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DestroyBullet();
        }
        if (other.gameObject.tag =="Wall")
        {
            Destroy(gameObject);
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
