using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_bullet : MonoBehaviour
{

    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject bullet;
    [SerializeField] private uint BULLET_FORCE;
    private float shootCounter = 0;
    [SerializeField] private float SHOOT_PER_SEC;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCounter * SHOOT_PER_SEC >= 1) {
            Shoot();
            shootCounter = 0;
        } else {
            shootCounter += Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject[] bullets = new GameObject[4];

        float x = trap.transform.lossyScale.x / 4;
        float y = bullet.transform.lossyScale.y / 2 + 0.001f;
        float z = trap.transform.lossyScale.x / 4;

        Vector3 v1 = trap.transform.TransformPoint(new Vector3(x, y, z));
        Vector3 v2 = trap.transform.TransformPoint(new Vector3(x, y, -z));
        Vector3 v3 = trap.transform.TransformPoint(new Vector3(-x, y, z));
        Vector3 v4 = trap.transform.TransformPoint(new Vector3(-x, y, -z));
        
        bullets[0] = Instantiate(bullet, v1, trap.transform.rotation);
        bullets[1] = Instantiate(bullet, v2, trap.transform.rotation);
        bullets[2] = Instantiate(bullet, v3, trap.transform.rotation);
        bullets[3] = Instantiate(bullet, v4, trap.transform.rotation);
        

        for (int i = 0; i < bullets.Length; ++i) {

            Vector3 a = bullets[0].transform.position;
            bullets[0].transform.Translate(new Vector3(0, 1, 0), Space.Self);
            Vector3 b = bullets[0].transform.position;
            Debug.Log(a + " " + b);
            Vector3 f = (b - a).normalized;
            bullets[0].transform.position = a;

            f *= BULLET_FORCE;
            f *= BULLET_FORCE;
            bullets[i].GetComponent<Rigidbody>().AddForce(f);
        }
    }

    GameObject[] bullets;
    bool b = false;

    void tempShoot()
    {
        if (b) {

        for (int i = 0; i < bullets.Length; ++i) {
            Vector3 a = bullets[0].transform.position;
            bullets[0].transform.Translate(new Vector3(0, 1, 0), Space.Self);
            Vector3 b = bullets[0].transform.position;
            Debug.Log(a + " " + b);
            Vector3 f = (b - a).normalized;
            bullets[0].transform.position = a;

            f *= BULLET_FORCE;
            Debug.DrawLine(a, a+f, Color.red);
        }
            return;
        }

        b = true;

        bullets = new GameObject[4];

        float x = trap.transform.lossyScale.x / 4;
        float y = bullet.transform.lossyScale.y / 2 + 0.001f;
        float z = trap.transform.lossyScale.x / 4;

        Vector3 v1 = new Vector3(x, y, z) + trap.transform.position;
        Vector3 v2 = new Vector3(x, y, -z) + trap.transform.position;
        Vector3 v3 = new Vector3(-x, y, z) + trap.transform.position;
        Vector3 v4 = new Vector3(-x, y, -z) + trap.transform.position;
        
        bullets[0] = Instantiate(bullet, v1, trap.transform.rotation);
        bullets[1] = Instantiate(bullet, v2, trap.transform.rotation);
        bullets[2] = Instantiate(bullet, v3, trap.transform.rotation);
        bullets[3] = Instantiate(bullet, v4, trap.transform.rotation);
    }
}
