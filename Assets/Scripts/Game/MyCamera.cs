using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public float speed;
    public float Z_MAX, Z_MIN;
    private void Update()
    {
        float h, v, s;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        s = Input.GetAxis("Mouse ScrollWheel");
        if (h != 0 || v != 0 || s != 0)
        {
            Vector3 pos = transform.position;
            pos += new Vector3(h, s * 5f, v) * speed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, Z_MIN, Z_MAX);
            transform.position = pos;
        }
    }
}
