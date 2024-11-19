using UnityEngine;

namespace Framework.UI
{
    public class SampleSimpleSingleton : MonoBehaviour
    { 
        private static SampleSimpleSingleton _instance;

        public static SampleSimpleSingleton Instance 
        { 
            get { return _instance; } 
        } 

        private void Awake() 
        {
            if (_instance != null && _instance != this) 
            { 
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
    }
}