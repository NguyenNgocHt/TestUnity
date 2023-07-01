using UnityEngine;

namespace Framework
{
    public class PDataResource : PDataBlock<PDataResource>
    {
        [SerializeField] PDataUnit<int> _resourceCoin;
        [SerializeField] PDataUnit<int> _resourceGem;

        protected override void Init()
        {
            base.Init();

            _resourceCoin = _resourceCoin ?? new PDataUnit<int>(InitializationConfig.InitCoin);
            _resourceGem = _resourceGem ?? new PDataUnit<int>(InitializationConfig.InitGem);
        }

        public static PDataUnit<int> GetResourceData(PResourceType type)
        {
            switch (type)
            {
                case PResourceType.Coin:
                    return Instance._resourceCoin;
                case PResourceType.Gem:
                    return Instance._resourceGem;
            }

            return null;
        }
    }
}