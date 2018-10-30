using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class SelectViewport : MonoBehaviour {
    #region Поля    
    private Vector3
        StartPos,
        EndPos;
    private Texture2D _WhiteTexture;

    private static System.Collections.Generic.List<SelectebleObject> list;
    public static System.Collections.Generic.List<SelectebleObject> List
    {
        get
        {
            if (list == null)
                list = new System.Collections.Generic.List<SelectebleObject>();
            return list;
        }
        set
        {
            list = value;
        }
    }

    [SerializeField]
    LayerMask
        SelectebleMask,
        GrassMask;
    private Camera _camera;
    public int Command;
    #endregion

    #region Свойства
    public Texture2D WhiteTexture
    {
        get
        {
            if (_WhiteTexture == null)
            {
                _WhiteTexture = new Texture2D(1, 1);
                _WhiteTexture.SetPixel(0, 0, new Color(0, 0, 1, .1f));
                _WhiteTexture.Apply();
            }
            return _WhiteTexture;
        }
    }
    #endregion
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
            EndPos = StartPos;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                EndPos = Input.mousePosition;
                Selecteble();
            }
            else
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(ForvardBuildTranslate(YG.YGPhysics.CameraRayToWorld(_camera, GrassMask).point));
            }
        }
    }
    private void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 max = Vector3.Max(StartPos, EndPos);
            Vector3 min = Vector3.Min(StartPos, EndPos);
            DrawRectViweport(min, max);
        }
    }
    private void DrawRectViweport(Vector3 min, Vector3 max)
    {
        GUI.DrawTexture(Rect.MinMaxRect(min.x, Screen.height - min.y, max.x, Screen.height - max.y), WhiteTexture);
    }
    private void Selecteble()
    {
        Camera c = Camera.main;
        var v1 = c.ScreenToViewportPoint(StartPos);
        var v2 = c.ScreenToViewportPoint(EndPos);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = c.nearClipPlane;
        max.z = c.farClipPlane;
        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);

        list.ForEach(v =>
        {
            if (v.Enum.Command == Command && bounds.Contains(c.WorldToViewportPoint(v.transform.position)))
                v.IsSelect = true;
            else
                v.IsSelect = false;
        });
    }


    IEnumerator ForvardBuildTranslate(Vector3 point)
    {
        while (!Input.GetMouseButtonUp(1))
            yield return null;
        var hit = YG.YGPhysics.CameraRayToWorld(_camera, GrassMask).point;
        Vector3 forvard = point - hit;
        TranslateUnitToPoint(hit, forvard);
    }
    private void TranslateUnitToPoint(Vector3 point, Vector3 forvard)
    {
        forvard.Normalize();
        GameObject go = new GameObject("Point");
        go.transform.position = point;
        go.transform.LookAt(go.transform.position + forvard);
        Vector3 right = go.transform.right;
        Destroy(go);

        var listmove = list.FindAll(v => v.IsSelect && v.NavigateObject != null);
        
        
        int count = listmove.Count;
        int countRow = Mathf.RoundToInt(Mathf.Pow(count, .5f)) + 1;
        for (int y = 0; y < countRow; y++)
        {
            for (int x = 0; x < countRow; x++)
            {
                int index = y * countRow + x;
                if(index < count)
                    listmove[y * countRow + x].NavigateObject.Translate(point - countRow / 2 * right + right * x + forvard * y);
            }
        }
    }
}
