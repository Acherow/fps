using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FPCamera : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform cam;

    public Vector3 camLookPos;

    public Image reticle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    public float x;
    private void FixedUpdate()
    {
        LookPos();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100 * Time.deltaTime;

        x -= mouseY;
        x = Mathf.Clamp(x, -90, 90);
        cam.localRotation = Quaternion.Euler(x, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void LookPos()
    {
        RaycastHit[] hits = Physics.RaycastAll(cam.position, cam.forward, 50).Where((c) => { return !c.collider.isTrigger; }).ToArray();
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
        if (hits.Length > 0)
        {
            reticle.color = Color.red;
            camLookPos = hits[0].point;
        }
        else
        {
            reticle.color = Color.white;
            camLookPos = cam.position + (cam.forward * 50);
        }
    }
}
