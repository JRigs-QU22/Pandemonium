using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throwing : MonoBehaviour
{

    public Animator anim;

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    private GameObject objectToThrow;
    public GameObject handgun;
    public GameObject shotgun;
    public GameObject smg;
    public ThirdPersonController tpc;

    [Header("Settings")]
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.T;
    public float throwForce;
    public float throwUpwardForce;
    public bool readyToThrow;

    private void Start()
    {
        anim = GetComponent<Animator>();
        readyToThrow = true;
    }

    private void Update()
    {
        try
        {
            // check the weapon the player is using
            for (int i = tpc.Weapons.Length - 1; i > -1; i--)
            {
                if (tpc.WeaponInUse == tpc.Weapons[0])
                {
                    objectToThrow = handgun;
                }
                else if (tpc.WeaponInUse == tpc.Weapons[2])
                {
                    objectToThrow = shotgun;
                }
                else if (tpc.WeaponInUse == tpc.Weapons[1])
                {
                    objectToThrow = smg;
                }
                else if (tpc.WeaponInUse == tpc.Weapons[-1]) //hand
                {
                    readyToThrow = false;
                    objectToThrow = null;
                }
            }
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine($"Hand: '{e}'");
        }

        if (Input.GetKeyDown(throwKey) && readyToThrow)
        {
            anim.SetTrigger("Throw");
            Invoke(nameof(Throw), 1);
            // after throw, turn off that weapon's visibility and lock it
            for (int i = tpc.Weapons.Length - 1; i > -1; i--)
            {
                if (i == tpc.WeaponID)
                {
                    tpc.Weapons[i].gameObject.SetActive(false);
                    tpc.Weapons[i].Unlocked = false;
                }
            }
            // set the state back to unarmed
            tpc.WeaponID = -1;
            tpc.WeaponInUse = null;
            tpc.IsArmed = false;
        }
    }

    private void Throw()
    {


        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse); 
    }

}