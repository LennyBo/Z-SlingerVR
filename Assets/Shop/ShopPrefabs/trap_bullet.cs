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

    private bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    public LayerMask stickableItems;

    void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            //Gizmos.matrix = rotationMatrix;

            float y = 0.05f;
            GameObject go = new GameObject();
            Transform f = go.transform;
            f.position = transform.position;
            f.rotation = transform.rotation;

            f.Translate(new Vector3(0, -y, 0), Space.Self);
            Vector3 scale = new Vector3(transform.lossyScale.x, y, transform.lossyScale.z);
            Gizmos.DrawWireSphere(f.position, y);
            bool b = Physics.CheckSphere(f.position, y, stickableItems);
            DestroyImmediate(go);
            Debug.Log(b);
        }
        
    // Update is called once per frame
    void Update()
    {
        if (!canShoot) {

            float y = 0.05f;
            GameObject go = new GameObject();
            Transform f = go.transform;
            f.position = transform.position;
            f.rotation = transform.rotation;

            f.Translate(new Vector3(0, -y, 0), Space.Self);
            Vector3 scale = new Vector3(transform.lossyScale.x, y, transform.lossyScale.z);
            Collider[] colliders = Physics.OverlapSphere(f.position, y, stickableItems);
            if (colliders.Length >= 1) {
                transform.SetParent(colliders[0].GetComponent<Transform>());
            }
            return;
        }
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
            //Debug.Log(a + " " + b);
            Vector3 f = (b - a).normalized;
            bullets[0].transform.position = a;

            f *= BULLET_FORCE;
            f *= BULLET_FORCE;
            bullets[i].GetComponent<Rigidbody>().AddForce(f);
        }
    }

}
