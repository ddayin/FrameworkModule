using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Framework.UI
{
    /// <summary>
    ///  텍스트만 있는 아이템(row)를 표시하고 ItemViewBase를 상속받는다.
    /// TODO: 구현 필요
    /// </summary>
    public class ItemViewText : ItemViewBase
    {
        public List<TextMeshProUGUI> m_ListText = new List<TextMeshProUGUI>();

        private void Awake()
        {
            
        }
        
        public void Init()
        {
            
        }

        public void SetData(RowStringData _rowStringData)
        {
            SetText(_rowStringData.m_ListRowData);
        }
        
        private void SetText(List<string> _listRowData)
        {
            for (int i = 0; i < _listRowData.Count; i++)
            {
                m_ListText[i].text = _listRowData[i];
            }
        }
    }
}