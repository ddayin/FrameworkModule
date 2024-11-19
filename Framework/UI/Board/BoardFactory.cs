using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    ///  게시판을 간편하게 생성하기 위한 팩토리 클래스
    /// </summary>
    public class BoardFactory
    {
        public GameObject m_PrefabPageBoardTable;
        public GameObject m_PrefabScrollBoardTable;
        
        public PageBoardTable CreatePageBoardTable(BoardType _boardType)
        {
            switch (_boardType)
            {
                case BoardType.User:
                    GameObject goUser = GameObject.Instantiate(m_PrefabPageBoardTable);
                    return goUser.GetComponent<PageBoardTable>();
                
                case BoardType.Device:
                    GameObject goDevice = GameObject.Instantiate(m_PrefabPageBoardTable);
                    return goDevice.GetComponent<PageBoardTable>();
                
                default:
                    Debug.LogError(_boardType.ToString());
                    break;
            }
            
            return null;
        }
    }
}