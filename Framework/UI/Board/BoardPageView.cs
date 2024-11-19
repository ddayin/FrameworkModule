using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    ///  게시판 페이지 하나를 표시한다.
    /// TODO: 구현 필요
    /// </summary>
    public class BoardPageView : MonoBehaviour
    {
        private List<ItemViewText> m_ListItemViewText = new List<ItemViewText>();
        
        public GameObject m_ItemViewTextPrefab;

        public Dictionary<string, RowStringData> m_DictionaryRowData = new Dictionary<string, RowStringData>();
        
        private void Awake()
        {
            
        }

        public void Init()
        {
            
        }

        public void CleanUp()
        {
        }

        public void SetDictionaryItem(Dictionary<string, RowStringData> _dictionaryRowData)
        {
            m_DictionaryRowData = _dictionaryRowData;
        }
        
        public void SetData()
        {
            foreach (var _dictionaryRowData in m_DictionaryRowData)
            {
                InstantiateItemViewText(_dictionaryRowData.Value);
            }
        }
        
        private void InstantiateItemViewText(RowStringData _rowStringData)
        {
            GameObject newItem = Instantiate(m_ItemViewTextPrefab,
                Vector3.zero, Quaternion.identity, this.transform);
            ItemViewText itemViewText = newItem.GetComponent<ItemViewText>();
            itemViewText.Init();
            itemViewText.SetData(_rowStringData);
            m_ListItemViewText.Add(itemViewText);
        }
    }
}