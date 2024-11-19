using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Framework.UI
{
    /// <summary>
    ///  모든 팝업들의 최상위 부모 클래스
    /// </summary>
    public class PopupBase : MonoBehaviour
    {
        public PopupBaseType m_Type;

        public Button m_ButtonClose = null;    // 닫기 버튼은 공통적으로 있음, 토스트 메시지 박스는 닫기 버튼 없음
        
        protected UI_AnimationPopup m_AnimationPopup;
        protected RectTransform m_RectTransform;

        private bool m_HasCloseButton = false;
        private UnityAction m_CloseAction;
        
        protected virtual void Awake()
        {
            m_AnimationPopup = GetComponent<UI_AnimationPopup>();
            m_RectTransform = GetComponent<RectTransform>();
        }

        public virtual void Init() {}

        public virtual void Init(PopupBaseType _type, bool _hasCloseButton = true)
        {
            this.m_Type = _type;
            m_HasCloseButton = _hasCloseButton;
            if (m_HasCloseButton == true)
            {
                m_ButtonClose.gameObject.SetActive(true);
                m_ButtonClose.onClick.AddListener(OnClickCloseButton);
            }
            else
            {
                m_ButtonClose.gameObject.SetActive(false);
            }
        }
        
        public virtual void Init(PopupBaseType _type, string _title, string _content, bool _hasCloseButton = true)
        {
            this.m_Type = _type;
            m_HasCloseButton = _hasCloseButton;
            if (m_HasCloseButton == true)
            {
                m_ButtonClose.gameObject.SetActive(true);
                m_ButtonClose.onClick.AddListener(OnClickCloseButton);
            }
            else
            {
                m_ButtonClose.gameObject.SetActive(false);
            }
        }

        public virtual void SetButtons(string _first, UnityAction _click_1, 
            string _second, UnityAction _click_2, 
            string _third, UnityAction _click_3) {}
        
        public virtual void SetSprite(Sprite _sprite, bool _isNativeSize = true) {}
        
        /// <summary>
        /// 여러 개의 팝업들이 열릴 때, 겹쳐서 보이지 않도록 위치를 조정
        /// </summary>
        /// <param name="count">stack 담긴 팝업 개수</param>
        public void SetStackedPosition(int count)
        {
            m_RectTransform.localPosition = new Vector3(40f, -40f, 0f) * count;
        }

        public void Open()
        {
            gameObject.SetActive(true);
            m_AnimationPopup.OnOpenPopup();
        }

        public void OnClickCloseButton()
        {
            m_AnimationPopup.OnClosePopup();
            Invoke("InvokeDisable", m_AnimationPopup.duration + 0.01f);
        }

        private void InvokeDisable()
        {
            Debug.Log("InvokeDisable() called");
            
            gameObject.SetActive(false);
        }

        public void HideCloseButton()
        {
            m_ButtonClose.gameObject.SetActive(false);
        }
    }
}