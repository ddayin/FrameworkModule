using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Framework.UI
{
    /// <summary>
    //  이미지를 포함한 팝업을 생성한다.
    /// TODO: 이미지 팝업은 하나의 버튼을 가지고 있는 제약이 있다. 추후 버튼 개수 개선할 것!
    /// TODO: 개념적으로는 PopupNormal을 상속 받는 것이 좋지만, 개발 편의상 중복 함수 존재하기 때문에 PopupBase 클래스를 상속 받는다.
    /// </summary>
    // public class PopupImage : PopupNormal
    public class PopupImage : PopupBase
    {
        private Button m_Button;
        
        [Header("Inspector 상에 필수로 걸어두어야 할 컴포넌트들")]
        public Image m_Image;
        public GameObject m_PrefabButton;
        public TextMeshProUGUI m_TextTitle;
        public TextMeshProUGUI m_TextContent;
        public Transform m_ParentOfButton;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Init() {}
        
        public override void Init(PopupBaseType _type, string _title, string _content, bool _hasCloseButton = true)
        {
            base.Init(_type, _title, _content, _hasCloseButton);
            
            if (_type == PopupBaseType.ButtonWithImage)
            {
                base.Init();
                
                this.m_Type = _type;
                this.m_TextTitle.text = _title;
                this.m_TextContent.text = _content;
                this.transform.localPosition = Vector3.zero;     // 팝업의 위치는 무조건 처음에는 화면 중앙에 보이도록 설정
                
                if (m_Button == null)
                {
                    m_Button = InstantiateButton();
                }
            }
            else
            {
                Debug.LogError(m_Type.ToString());
            }
        }
        
        public override void SetSprite(Sprite _sprite, bool _isNativeSize = true)
        {
            if (_sprite == null) return;
            
            this.m_Image.sprite = _sprite;
            if (_isNativeSize == true)
            {
                this.m_Image.SetNativeSize();
            }
        }

        public override void SetButtons(string _first, UnityAction _click_1, string _second, UnityAction _click_2, string _third, UnityAction _click_3)
        {
            SetButton(_first, _click_1);
        }
        
        private Button InstantiateButton()
        {
            GameObject newButton = Instantiate(m_PrefabButton, Vector3.zero, Quaternion.identity, m_ParentOfButton);
            return newButton.GetComponent<Button>();
        }
        
        private void SetButton(string text, UnityAction click)
        {
            m_Button.onClick.AddListener(click);
        }
    }
}
