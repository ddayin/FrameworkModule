using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    /// 프로젝트에서 사용하는 게시판 타입
    /// </summary>
    public enum BoardType
    {
        User = 0,   // 사용자 관리 게시판
        Device      // 기기 관리 게시판
    }
    
    [Serializable]
    public class RowStringData
    {
        /// <summary>
        /// TODO: row 데이터의 타입이 string이 아니고, 이미지라면 어떻게 처리해야 할까?
        /// 우선 string만 가능하도록 구현하자.
        /// </summary>
       public List<string> m_ListRowData = new List<string>();
    }

    [Serializable]
    public class RowObjectData
    {
        /// <summary>
        /// TODO: string이 될 수도 있고 Sprite가 될 수도 있게
        /// Object는 Sprite 등이 될 수 있다.
        /// </summary>
        public List<System.Object> m_ListRowData = new List<System.Object>();
    }
    
    /// <summary>
    ///  row와 column으로 짜여진 테이블 데이터 정의
    /// TODO: 구현 필요
    /// </summary>
    public class TableModel : MonoBehaviour
    {
        /// <summary>
        /// key는 row의 No, value는 row의 문자열 데이터
        /// </summary>
        public Dictionary<string, RowStringData> m_DicRowStringData = new Dictionary<string, RowStringData>();
        public Dictionary<string, RowObjectData> m_DicRowUIData = new Dictionary<string, RowObjectData>();
        
        public List<string> m_ListColumnHeader = new List<string>();
        
        /// <summary>
        /// 테스트 용도로, 실제로는 서버와 통신하여 모든 게시판 데이터를 받아와야 한다.
        /// </summary>
        public ScriptableObject m_ScriptableObject;
        
        private void Awake()
        {
            
        }

        /// <summary>
        /// 초기화 필수
        /// </summary>
        public void Init()
        {
            
        }

        /// <summary>
        /// 해제 필수
        /// </summary>
        public void CleanUp()
        {
            
        }

        /// <summary>
        /// [중요] 게시판 데이터를 로컬에 있는 scriptable object로부터 로드한다.
        /// 왜냐하면 json 보다는 유니티에서 권장하는 scriptable object를 사용하는 것이 좋다.
        /// </summary>
        public void LoadScriptableObjectForTest()
        {
            Resources.Load<ScriptableObject>("Assets/EasySpreadsheet/GeneratedData/Resources/DataSheet");
        }
        
        /// <summary>
        /// [중요] 서버로부터 json 데이터를 로드한다.
        /// </summary>
        public void LoadJsonFromServer()
        {
            
        }

        /// <summary>
        /// 개발을 위한 테스트 용도로 데이터를 로드한다.
        /// </summary>
        public void LoadTestData()
        {
            
        }
        
        public string GetColumnName(int _index)
        {
            return m_ListColumnHeader[_index];
        }
        
        public RowStringData GetRowDataByNo(string _no)
        {
            return m_DicRowStringData[_no];
        }

        public Dictionary<string, RowStringData> GetDictionaryRowData()
        {
            return m_DicRowStringData;
        }
        
        public RowStringData SortByNo()
        {
            return null;
        }
    }
}