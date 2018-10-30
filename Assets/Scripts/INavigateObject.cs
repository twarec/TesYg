using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigateObject
{
    void Translate(Vector3 pos);
    float Speed { get; set; }
}
