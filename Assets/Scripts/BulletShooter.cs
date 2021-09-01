    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shootForce = 20000f;
    public float timePerShot;

    private float timeSinceLastShot = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(timePerShot);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0.9f && timeSinceLastShot > timePerShot)
        {
            timeSinceLastShot = 0;
            GameObject g = Instantiate(bulletPrefab, transform.position + transform.forward * 1, transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(transform.position + transform.forward * 1* shootForce);
        }
    }
}
