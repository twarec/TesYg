using UnityEngine;

namespace Game
{
    public class ResoursesManager : MonoBehaviour
    {
        public static ResoursesManager obj;
        private void Awake()
        {
            obj = this;
        }
        #region Поля
        [SerializeField]
        private int
            _Gold,
            _Iron,
            _Energy,
            _Fuel;

        #endregion
        #region Свойства
        public int Gold
        {
            get
            {
                return _Gold;
            }
            set
            {
                _Gold = value;
                if(GoldAction != null)
                    GoldAction(_Gold);
            }
        }
        public int Iron
        {
            get
            {
                return _Iron;
            }
            set
            {
                _Iron = value;
                if (IronAction != null)
                    IronAction(_Iron);
            }
        }
        public int Energy
        {
            get
            {
                return _Energy;
            }
            set
            {
                _Energy = value;
                if (EnergyAction != null)
                    EnergyAction(_Energy);
            }
        }
        public int Fuel
        {
            get
            {
                return _Fuel;
            }
            set
            {
                _Fuel = value;
                if (FuelAction != null)
                    FuelAction(_Fuel);
            }
        }
        #endregion
        #region Actions
        public System.Action<int>
            GoldAction,
            IronAction,
            EnergyAction,
            FuelAction;
        #endregion
    }
}
