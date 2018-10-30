using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace YGScripts {
    public class Rotate : MonoBehaviour
    {
        public Transform[] transforms;
        public Vector3 rot;
        public float duration;
        public int count;
        private void Start()
        {
            foreach (var v in transforms)
                v.DORotate(rot, duration).SetLoops(count, LoopType.Incremental).SetEase(Ease.Linear);
        }
    }
}
