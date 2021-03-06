using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public GameObject LaserPosition;

    public LayerMask CrosshairHitMask;
    public CamPivotController MyPivotCamera;
    public Transform MyCamera;

    public ThirdPersonController tpc;

    // Start is called before the first frame update
    public void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;

        MyPivotCamera = FindObjectOfType<CamPivotController>();
        MyCamera = MyPivotCamera.mCamera.transform;
        tpc = FindObjectOfType<ThirdPersonController>();
        CrosshairHitMask = FindObjectOfType<ThirdPersonController>().CrosshairHitMask;
    }

    // Update is called once per frame
    public void Update()
    {
        RaycastHit CrosshairHit;

        if (Physics.Raycast(MyCamera.transform.position + MyCamera.transform.forward * MyPivotCamera.Distance / 2, MyCamera.transform.forward, out CrosshairHit, 500, CrosshairHitMask))
        {
            this.transform.LookAt(CrosshairHit.point);
            lr.SetPosition(1, new Vector3(0, 0, CrosshairHit.distance));
        }
          

    }
}
