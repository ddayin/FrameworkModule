using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Framework.UI
{
    /// <summary>
    ///  최대 버튼 3개를 가지고 있는 일반적인 팝업
    /// TODO: Object.InstantiateAsync()를 사용하여 비동기로 생성하도록 수정해야 한다.
    /// </summary>
    public class PopupNormal : PopupBase
    {
        private Button[] m_ButtonsArray = new Button[3];    // 버튼이 최대 3개까지만 생성 가능하다.

        [Header("Inspector 상에 필수로 걸어두어야 할 컴포넌트들")]
        public GameObject m_PrefabButton;
        public TMPro.TextMeshProUGUI m_TextTitle;
        public TMPro.TextMeshProUGUI m_TextContent;
        public Transform m_ParentOfButtons;

        private UnityAction[] m_ClickActions = new UnityAction[3];

        protected override void Awake()
        {
            base.Awake();
            
        }

        /// <summary>
        /// 초기화 하면서 팝업 내 버튼을 팝업 타입에 따라 생성해준다.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_title"></param>
        /// <param name="_content"></param>
        public override void Init(PopupBaseType _type, string _title, string _content, bool _hasCloseButton = true)
        {
            base.Init(_type, _title, _content, _hasCloseButton);
            
            this.m_Type = _type;
            this.m_TextTitle.text = _title;
            this.m_TextContent.text = _content;
            this.transform.localPosition = Vector3.zero;     // 팝업의 위치는 무조건 처음에는 화면 중앙에 보이도록 설정
            
            if (base.m_Type == PopupBaseType.NormalButtonOne)
            {
                if (m_ButtonsArray[0] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[0] = button;
                }
            }
            else if (base.m_Type == PopupBaseType.NormalButtonTwo)
            {
                if (m_ButtonsArray[0] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[0] = button;
                }
                if (m_ButtonsArray[1] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[1] = button;
                }
            }
            else if (base.m_Type == PopupBaseType.NormalButtonThree)
            {
                if (m_ButtonsArray[0] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[0] = button;
                }
                if (m_ButtonsArray[1] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[1] = button;
                }
                if (m_ButtonsArray[2] == null)
                {
                    Button button = InstantiateButton();
                    m_ButtonsArray[2] = button;
                }
            }
            else
            {
                Debug.LogError(_type.ToString());
            }
        }

        /// <summary>
        ///  버튼 내에 텍스트 설정
        /// </summary>
        /// <param name="_first"></param>
        /// <param name="_second"></param>
        /// <param name="_third"></param>
        public override void SetButtons(string _first, UnityAction _click_1,
            string _second, UnityAction _click_2, 
            string _third, UnityAction _click_3)
        {
            SetButton(0, _first, _click_1);
            SetButton(1, _second, _click_2);
            SetButton(2, _third, _click_3);
        }

        /// <summary>
        /// 테스트 용도
        /// </summary>
        public void SetRandomPosition()
        {
            int seed = (int) (DateTime.Now.Ticks & 0x0000FFFF);
            System.Random random = new System.Random(seed);
            int width = (int) (Screen.width * 0.5f);
            int height = (int) (Screen.height * 0.5f);
            
            int x = random.Next(-width, width);
            int y = random.Next(-height, height);

            RectTransform rt = this.transform as RectTransform;
            rt.anchoredPosition = new Vector2(x, y);
        }
        
        private Button InstantiateButton()
        {
            GameObject newButton = Instantiate(m_PrefabButton, Vector3.zero, Quaternion.identity, m_ParentOfButtons);
            Button button = newButton.GetComponent<Button>();
            return button;
        }

        private void SetButton(int _index, string _text, UnityAction _click)
        {
            if (String.IsNullOrEmpty(_text) == true) return;
            if (_click == null) return;

            TMPro.TextMeshProUGUI buttonText = m_ButtonsArray[_index].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            buttonText.text = _text;

            m_ClickActions[_index] = _click;
        }
    }
}
