using UnityEngine;
using DG.Tweening;

public class Build : MonoBehaviour {
    private bool init;
    public virtual bool Init
    {
        get
        {
            return init;
        }
        set
        {
            init = value;
            if (init)
            {
                gameObject.layer = 9;
                InitObject();
            }
        }
    }
    public string Name;
    public float Timer;
    public GameObject Body;
    public System.Action OnComlite;
    protected virtual void InitObject()
    {
        GetComponentsInChildren<Renderer>().ForeAche((v) =>
        {
            Material material = v.material;
            v.material = MaterialManager.Obj.BuildCompite;
            DOTween.To(() => 1f, (x) => v.material.SetFloat("Vector1_3255E0C7", x), -1f, 3f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                v.material = material;
                OnComlite?.Invoke();
            });
        });
    }


    [SerializeField]
    private int
        _PriceGold;

    public int PriceGold
    {
        get
        {
            return _PriceGold;
        }
        set
        {
            _PriceGold = value;
        }
    }
}
