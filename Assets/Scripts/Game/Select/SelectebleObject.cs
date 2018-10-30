using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectebleObject : MonoBehaviour
{
    /// <summary>
    /// Иконка объекта
    /// </summary>
    [Header("Иконка объекта")]
    public Sprite sprite;

    /// <summary>
    /// Скорость передвижения
    /// </summary>
    [Header("Скорость передвижения")]
    public float speed;

    #region Селектирование объекта
    [SerializeField]
    [Header("Селектирование объекта")]
    private bool isSelect;
    public bool IsSelect
    {
        get
        {
            return isSelect;
        }
        set
        {
            if (isSelect != value)
            {
                isSelect = value;
                if (value)
                {
                    UIManager.obj.SetUnit(this);
                }
                else
                {
                    UIManager.obj.RemoveUnit(this);
                }
            }
        }
    }
    #endregion
    /// <summary>
    /// Имя(индекс)
    /// </summary>
    [Header("Имя(индекс)")]
    public string Name;

    private INavigateObject navigateObject;

    private DestroydObject destroydObject;

    [SerializeField] private Enum _enum;
    public INavigateObject NavigateObject => navigateObject;
    public DestroydObject DestroydObject => destroydObject;
    public Enum Enum => _enum;

    private void Start()
    {
        gameObject.layer = 10;
        if (navigateObject != null)
            navigateObject.Speed = speed;
    }
    private void Awake()
    {
        navigateObject = _enum.GetComponent<INavigateObject>();
        destroydObject = _enum.GetComponent<DestroydObject>();
        SelectViewport.List.Add(this);
    }
    private void OnDestroy()
    {
        SelectViewport.List.Remove(this);
    }
 
}
