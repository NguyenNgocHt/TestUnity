using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class PDataUnit<T>
    {
        [SerializeField] T _data;

        public T Data
        {
            get { return _data; }
            set
            {
                if (!_data.Equals(value))
                {
                    _data = value;
                    OnDataChanged?.Invoke(_data);
                }
            }
        }

        public event Callback<T> OnDataChanged;

        public PDataUnit(T defaultValue)
        {
            _data = defaultValue;
        }
    }
}