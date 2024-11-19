using UnityEngine;
using UnityEngine.UI;

using Framework.UI;

namespace Framework.UI
{
    /// <summary>
    ///  대화창 팝업
    /// </summary>
    public class DialoguePopup : PopupBase
    {
        [Header("Inspector 상에 필수로 설정해야 하는 것들")]
        public TMPro.TextMeshProUGUI titleText;     // 타이틀 텍스트는 필요 없을듯, 하지만 닉네임은 필요할 수 있음
        public TMPro.TextMeshProUGUI contentText;   // 필수
        
        public Image imageCharacter;
        
        protected override void Awake()
        {
            base.Awake();
        }
        
        public override void Init(PopupBaseType _type, string _title, string _content, bool _hasCloseButton = true)
        {
            base.Init(_type, _title, _content, _hasCloseButton);
            
            titleText.text = _title;
            contentText.text = _content;
        }
        
        /// <summary>
        /// 대화상자의 캐릭터 이미지를 설정한다.
        /// </summary>
        /// <param name="sprite"></param>
        public void SetCharacter(Sprite sprite)
        {
            imageCharacter.sprite = sprite;
            imageCharacter.SetNativeSize();
            imageCharacter.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);        // 임시 코드
        }
    }
}