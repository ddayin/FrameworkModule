using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    ///  TableModel를 가지고 있는 게시판
    /// TODO: 구현 필요
    /// </summary>
    public class PageBoardTable : MonoBehaviour
    {
        public TableModel m_TableModel;
        public BoardType m_BoardType;
        public int m_FixedRowNumber = 10;

        private void Awake()
        {

        }

        /// <summary>
        /// 초기화 필수
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_rowCount"></param>
        public void Init(BoardType _type, TableModel _model, int _rowCount)
        {
            m_BoardType = _type;
            m_TableModel = _model;
            m_FixedRowNumber = _rowCount;

            SplitModelByRowCount();
        }

        /// <summary>
        /// 해제 필수
        /// </summary>
        public void CleanUp()
        {
            
        }

        /// <summary>
        /// 지정한 row 갯수만큼 데이터를 나누어서 List로 반환한다.
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, RowStringData>> SplitModelByRowCount()
        {
            Dictionary<string, RowStringData> dictionary = m_TableModel.GetDictionaryRowData();
            var dividedDictionary = dictionary
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(x => x.Index / m_FixedRowNumber)
                .Select(g => g.Select(x => x.Value).ToDictionary(x => x.Key, x => x.Value))
                .ToList();

            return dividedDictionary;
        }
    }
}