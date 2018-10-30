using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public static class YGPhysics
    {
        public static RaycastHit CameraRayToWorld(Camera camera)
        {
            RaycastHit hit;
            Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit);
            return hit;
        }
        public static RaycastHit CameraRayToWorld(Camera camera,LayerMask layerMask)
        {
            RaycastHit hit;
            Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000, layerMask);
            return hit;
        }
    }
}
