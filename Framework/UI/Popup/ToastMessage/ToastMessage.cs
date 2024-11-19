using UnityEngine;
using TMPro;
using UnityCommunity.UnitySingleton;

namespace Framework.UI
{
    /// <summary>
    ///  토스트 메시지는 다른 팝업과 다르게 자동으로 사라지는 메시지이다.
    /// 싱글턴 패턴이 반영되어 있는 이유는 토스트 메시지는 어디서든지 호출이 가능해야 하기 때문이다.
    /// TODO: 토스트 메시지가 여러 개가 나오는 경우를 대비하여 토스트 메시지를 여러 개 생성할 수 있도록 수정해야 한다. 우선은 동작하는 것부터.
    /// </summary>
    public class ToastMessage : PersistentMonoSingleton<ToastMessage>
    {
        [Header("Inspector 상에 필수로 설정")]
        public TextMeshProUGUI m_TextMessage;
        public float m_LifeTime = 1f;

        private string m_Message;

        protected override void Awake()
        {
            base.Awake();

            if (m_TextMessage == null)
            {
                m_TextMessage = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                Debug.LogError("m_TextMessage : " + m_TextMessage);
            }
            
            this.gameObject.SetActive(false);
            // SetPositionAsCenter();
        }

        public void Init(string _message)
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }

            this.transform.SetParent(this.transform.parent);
            this.m_Message = _message;
            this.m_TextMessage.text = m_Message;

            Invoke("Close", m_LifeTime);
        }

        public void CleanUp()
        {
            
        }

        public void Set(string _message)
        {
            this.m_Message = _message;
            m_TextMessage.text = this.m_Message;
        }

        public void SetPositionAsCenter()
        {
            this.transform.localPosition = Vector3.zero;
        }

        public void SetPositionAsBottom()
        {
            this.transform.localPosition = new Vector3(0f, 90f, 0f);
        }

        public void Open()
        {
            this.gameObject.SetActive(true);
        }
        
        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}
