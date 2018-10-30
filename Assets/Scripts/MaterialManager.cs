using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{

    public static MaterialManager Obj { get; private set; }
    private void Awake()
    {
        Obj = this;
    }
    public Material
        BuildCompite,
        BuildPosiyion;

}
