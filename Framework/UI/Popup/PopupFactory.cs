using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    /// parameter가 너무 많아서 사용자 입장에서 복잡해 보임
    /// TODO: 보류
    /// </summary>
    public class PopupFactory
    {
        public PopupNormal Create(GameObject prefabPopup, PopupBaseType type, 
            string title, string content, Transform transformParent, bool _hasCloseButton = true)
        {
            PopupNormal popupNormal = null;
            
            GameObject newPopup = CommonUtils.Instantiate(prefabPopup, transformParent);
            popupNormal = newPopup.transform.GetComponent<PopupNormal>();
            popupNormal.Init(type, title, content, _hasCloseButton);
            
            return popupNormal;
        }
    }
}
