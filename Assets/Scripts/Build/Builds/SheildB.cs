using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildB : Build
{
    public Transform Spheresheild;
    public override bool Init
    {
        get
        {
            return base.Init;
        }

        set
        {
            base.Init = value;
            OnComlite += () =>
            {
                Spheresheild.gameObject.SetActive(true);
            };
        }
    }
}
