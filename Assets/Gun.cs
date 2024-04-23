using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float reloadtime;
    public float curreload;
    bool reloading;

    public float cooldowntime;
    float cooldown;

    public float speed;

    public int maxammo;
    public int ammo;
    [HideInInspector]
    public Vector3 aimpoint;

    public Transform muzzlespot;
    public GameObject proj;

    private void Start()
    {
        ammo = maxammo;
    }

    private void Update()
    {
        if(reloading)
        {
            if (curreload > 0)
                curreload -= Time.deltaTime;
            else
            {
                reloading = false;
                curreload = 0;
                ammo = maxammo;
            }
        }

        if (cooldown > 0)
            cooldown -= Time.deltaTime;

    }

    public void Shoot()
    {
        if (ammo > 0 && !reloading && cooldown <= 0)
        {
            cooldown = cooldowntime;
            ammo--;
            GameObject g = Instantiate(proj, muzzlespot.position, Quaternion.LookRotation(transform.up, aimpoint - transform.position));
            g.GetComponent<Rigidbody>().AddForce(((aimpoint - muzzlespot.transform.position).normalized * speed), ForceMode.Impulse);
        }
    }

    public void Reload()
    {
        reloading = true;
        curreload = reloadtime;
    }
}
