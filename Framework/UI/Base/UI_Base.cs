using System;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    /// 
    /// TODO: 모든 Unity UGUI 컴포넌트는 UI_Base를 상속받는다.
    /// </summary>
    public class UI_Base : MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }
        
        protected virtual void Start()
        {
            
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
        
        public virtual void OnPause()
        {
            
        }
        
        public virtual void OnResume()
        {
            
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }
    }
}
