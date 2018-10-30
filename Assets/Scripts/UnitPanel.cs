using UnityEngine;
using UnityEngine.UI;

public class UnitPanel : MonoBehaviour
{
    [SerializeField]
    private Image hp;
    public float Hp
    {
        get
        {
            if (hp)
            {
                return hp.fillAmount;
            }
            else
            {
                return -1;
            }
        }
        set
        {
            if (hp)
            {
                if (value < 0)
                    Destroy(hp.gameObject);
                hp.fillAmount = value;
            }
        }
    }
    [SerializeField]
    private Image icon;
    public Sprite Icon
    {
        get
        {
            return icon.sprite;
        }
        set
        {
            icon.sprite = value;
        }
    }
    [HideInInspector]
    public SelectebleObject unit;
    /// <summary>
    /// поле для Кол-во экземпляров
    /// </summary>
    [SerializeField]
    [Header("Кол-во экземпляров")]
    private Text CountText;
    /// <summary>
    /// Кол-во экземпляров
    /// </summary>
    private int count;
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            if (count == 1)
                CountText.gameObject.SetActive(false);
            else
            {
                CountText.gameObject.SetActive(true);
                CountText.text = count.ToString();
            }
        }
    }
}
