using System.Collections;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    #region Поля
    private Build buildSelect;
    [SerializeField]
    private BuildDB buildDB;
    [SerializeField]
    private LayerMask buldLayer;
    [SerializeField]
    private Material UnitBuildMaterial;
    public static BuildManager obj;
    #endregion
    #region Свойства
    public Build BuildSelect
    {
        get
        {
            return buildSelect;
        }
        set
        {
            if (buildSelect == null || value == null)
            {
                buildSelect = value;
                if (buildSelect != null)
                    StartCoroutine(IECreateAndSelectBuild());
            }
        }
    }
    #endregion

    #region Выбор положения для постройки здания
    public System.Action<Build> BuildComplite;
    private IEnumerator IECreateAndSelectBuild()
    {
        Cursor.visible = false;
        UIManager.obj.SetVisibleBuildePanel(false);
        Camera _camera = Camera.main;
        buildSelect = Instantiate(buildSelect);
        Transform transform = buildSelect.transform;
        while(buildSelect != null)
        {
            RaycastHit hit = YG.YGPhysics.CameraRayToWorld(_camera, buldLayer);
            if (hit.transform.gameObject.layer != 9)
            {
                Vector3 pos = hit.point;
                pos.x = Mathf.RoundToInt(pos.x);
                pos.z = Mathf.RoundToInt(pos.z);
                transform.position = pos;
                if (Input.GetMouseButtonUp(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    Build();
                if (Input.GetMouseButtonUp(1))
                {
                    Destroy(transform.gameObject);
                    BuildSelect = null;
                    break;
                }
            }
            yield return null;
        }
        Cursor.visible = true;
        UIManager.obj.SetVisibleBuildePanel(true);
    }
    private void Build()
    {
        buildSelect.Init = true;
        buildSelect = null;
        if (BuildComplite != null)
            BuildComplite(buildSelect);
    }
    #endregion

    private void Awake()
    {
        obj = this;
    }
    private void Start()
    {
        UIManager.obj.AddBuildRange(buildDB.AllBuild.ToArray());
    }
}
