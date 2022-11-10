using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPoint;
    public Camera eyesCam;

    public float shootingforce;
    public float upwardforce;

    public Vector3 reference;

    public float timeBetweenshots;
    public float effectedTimeBetweenShots = 1f;

    public float range = 100f;
    private void Awake()
    {
        bulletPoint = GameObject.Find("GunBarrel").transform;
    }
    private void Update()
    {
        timeBetweenshots -= Time.deltaTime;

    }
    public void Shoot()
    {
        if (timeBetweenshots <= 0)
        {
            Ray ray = eyesCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 directionWithNoSpread = targetPoint - bulletPoint.position;
            Vector3 reference = directionWithNoSpread;
            GameObject currentBullet = Instantiate(bullet, bulletPoint.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithNoSpread.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithNoSpread.normalized * shootingforce, ForceMode.Impulse);

            timeBetweenshots = effectedTimeBetweenShots;
        }
        else if (timeBetweenshots > 0)
        {
            return;
        }
    }
    public void ShootFaster()
    {
        effectedTimeBetweenShots *= 0.9f;
    }
}
