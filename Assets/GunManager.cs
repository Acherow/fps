using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public Gun activegun;
    FPCamera cam;
    public List<Gun> guns;

    private void Start()
    {
        cam = GetComponentInParent<FPCamera>();
        SwitchGun(0);
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            activegun.Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            activegun.Reload();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0 && guns.Count > 0)
        {
            SwitchGun((guns.IndexOf(activegun) + (int)Input.GetAxisRaw("Mouse ScrollWheel")) % guns.Count);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchGun(3);

        if (activegun)
            activegun.aimpoint = cam.camLookPos;
    }

    public void SwitchGun(int id)
    {
        if (id < 0)
            id = guns.Count - Mathf.Abs(id);

        if (id < guns.Count && guns[id] != null)
        {
            for (int i = 0; i < guns.Count; i++)
            {
                guns[i].gameObject.SetActive(i == id);
            }
            activegun = guns[id];
        }
    }
}
