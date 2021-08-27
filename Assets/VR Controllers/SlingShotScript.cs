using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collision : MonoBehaviour
{

    public GameObject front;
    public Material closeMaterial;
    public Material farMaterial;
    public GameObject bullet;
    public InputActionReference pullAction;
    public float lineWidth = 0.012f;
    public int forceMultplier = 1000;

    private bool _canPull;
    private bool _isPulling;
    private LineRenderer lineRenderer;

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
                front.GetComponent<Renderer>().material = closeMaterial;
            }
            else
            {
                front.GetComponent<Renderer>().material = farMaterial;
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
        lineRenderer = GetComponent<LineRenderer>();
        pullAction.action.started += Action_started;
        pullAction.action.canceled += Action_canceled;
    }


    private void Action_canceled(InputAction.CallbackContext obj)
    {
        if (isPulling && !canPull) //!canPull in case the user wants to cancel the shot
        {
            Debug.Log("Shoot");
            shoot();
            isPulling = false;
        }
        else
        {
            Debug.Log("Cancled Shot");
            isPulling = false;
        }
    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        if (canPull)
        {
            Debug.Log("Start pull");
            isPulling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, front.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        //lineLength = (-front.transform.position + back.transform.position).magnitude;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == front)
        {
            canPull = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == front)
        {
            canPull = false;
        }
    }

    private void shoot()
    {
        Vector3 pullVector = front.transform.position - transform.position;
        Vector3 bulletStart = transform.position + (pullVector).normalized * 0.1f;
        GameObject g = Instantiate(bullet, bulletStart, new Quaternion(0, 0, 0, 0));
        g.GetComponent<Rigidbody>().AddForce(pullVector* forceMultplier);
    }

    void setLineSize(float size)
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, size);
        lineRenderer.widthCurve = curve;
    }
}
