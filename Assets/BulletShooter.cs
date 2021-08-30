using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.9f)
        {
            Vector3 bulletSpawnPos = transform.position + (transform.rotation.eulerAngles).normalized;
            Instantiate(bulletPrefab, bulletSpawnPos, new Quaternion());
        }
    }
}
