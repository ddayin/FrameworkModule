using UnityCommunity.UnitySingleton;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 여러 종류의 에셋이 있을텐데 종류마다 Loader 클래스가 분리되어 있다면,
    /// 중복된 코드들이 많았을 것이다.
    /// 그래서 우선 base가 되는 클래스를 만들어 두었다.
    /// </summary>
    public class BaseLoader : PersistentMonoSingleton<BaseLoader>
    {
        protected override void Awake()
        {
            base.Awake();
        }
    }
}