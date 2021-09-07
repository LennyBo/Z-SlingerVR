using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotScript : MonoBehaviour
{

    public List<GameObject> fronts;
    public GameObject stonePrefab;
    public Material closeMaterial;
    public Material farMaterial;
    public GameObject bullet;
    public InputActionReference pullAction;
    public InputActionReference pullToggle;
    public float lineWidth = 0.012f;
    public int forceMultplier = 1000;


    private bool _canPull;
    private bool _isPulling;
    private int massMultiplier = 1;
    internal bool hasUgrapdedMultiShot = false;
    internal bool hasUgrapdedDamage = false;


    private bool isPulling
    {
        set
        {
            if (value)
            {
                setLineSize(lineWidth);
            }
            else
            {
                setLineSize(0); //Hides the line
            }
            _isPulling = value;
        }
        get
        {
            return _isPulling;
        }
    }

    private bool canPull { 
        set {
            if (value)
            {
                foreach (var front in fronts)
                {
                    front.GetComponent<Renderer>().material = closeMaterial;
                }
            }
            else
            {
                foreach (var front in fronts)
                {
                    front.GetComponent<Renderer>().material = farMaterial;
                }
            }
            _canPull = value;
        } 
        get {
            return _canPull;
        } 
    }



    // Start is called before the first frame update
    void Start()
    {
        pullAction.action.started += Action_started;
        pullAction.action.canceled += Action_canceled;
        if(pullToggle != null)
        {
            pullToggle.action.started += Action_started1;
        }
        //upgradeToMultiShot();
        //upgradeDamage();
    }

    private void Action_started1(InputAction.CallbackContext obj)
    {
        if (canPull)
        {
            //Debug.Log("Start pull");
            isPulling = true;
        }
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        if (isPulling && !canPull) //!canPull in case the user wants to cancel the shot
        {
            //Debug.Log("Shoot");
            shoot();
            isPulling = false;
        }
        else
        {
            //Debug.Log("Cancled Shot");
            isPulling = false;
        }
    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        if (canPull)
        {
            //Debug.Log("Start pull");
            isPulling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var front in fronts)
        {
            LineRenderer lineRenderer = front.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, front.transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
        
        //lineLength = (-front.transform.position + back.transform.position).magnitude;
    }

    void OnTriggerEnter(Collider other)
    {
        if (fronts.IndexOf(other.gameObject) != -1)
        {
            canPull = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (fronts.IndexOf(other.gameObject) != -1)
        {
            canPull = false;
        }
    }

    private void shoot()
    {
        foreach (var front in fronts)
        {
            Vector3 pullVector = front.transform.position - transform.position;
            Vector3 bulletStart = transform.position + (pullVector).normalized * 0.1f;
            GameObject g = Instantiate(bullet, bulletStart, new Quaternion(0, 0, 0, 0));
            g.GetComponent<Rigidbody>().mass *= 2;
            g.GetComponent<Rigidbody>().AddForce(pullVector * forceMultplier);
        }
    }

    void setLineSize(float size)
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, size);
        foreach (var front in fronts)
        {
            LineRenderer lineRenderer = front.GetComponent<LineRenderer>();
            lineRenderer.widthCurve = curve;
        }
        
    }

    public void upgradeDamage()
    {
        if (hasUgrapdedDamage)
            return; //Can't upgrade twice
        //Doubles mass and force
        hasUgrapdedDamage = true;
        massMultiplier *= 2;
        forceMultplier *= 2;
    }

    public void upgradeToMultiShot()
    {
        if (hasUgrapdedMultiShot)
            return; //Can't upgrade twice
        //Set 2 new stones on to the prefab
        GameObject stone1 = Instantiate(stonePrefab, fronts[0].transform.parent);
        GameObject stone2 = Instantiate(stonePrefab, fronts[0].transform.parent);
        //Move them apart
        Vector3 v1 = new Vector3(0.05f, 0, 0);
        Vector3 v2 = new Vector3(-0.05f, 0, 0);
        stone1.transform.localPosition = v1;
        stone2.transform.localPosition = v2;

        fronts.Add(stone1);
        fronts.Add(stone2);
        hasUgrapdedMultiShot = true;
    }
}
