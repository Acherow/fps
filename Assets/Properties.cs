using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Properties : MonoBehaviour
{
    TMP_Text txt;

    public Controller ctrl;
    public GunManager guns;

    private void Start()
    {
        txt = GetComponent<TMP_Text>();
    }

    void Update()
    {
        txt.text = $"Vida: {ctrl.health}\nArma: {guns.activegun.name}\nMunição: {guns.activegun.ammo}/{guns.activegun.maxammo}\nReload: {guns.activegun.curreload.ToString("0.##")}";
    }
}
