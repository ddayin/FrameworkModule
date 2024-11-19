using System;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    /// <summary>
    /// TODO: 구현 필요
    /// </summary>
    public class UI_Button : UI_Base
    {
        private Button button;
        
        protected override void Awake()
        {
            base.Awake();
            
            button = GetComponent<Button>();
            
            button.onClick.AddListener(OnClickButton);
        }
        
        private void OnClickButton()
        {
            Debug.Log("Button Clicked");
        }
    }
}
