using System;
using Framework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Test
{
    /// <summary>
    ///  테스트 용도 / 팝업 프레임워크를 사용하는 방법을 보여주는 클래스
    /// 인벤토리 팝업, 설정 팝업, 토스트 메시지, 대화상자 팝업, 퀘스트 팝업 등을 호출할 수 있는 메뉴이다.
    /// </summary>
    public class PopupTester : MonoBehaviour
    {
        [Header("Inspector 상에 필수로 설정")]
        
        [Header("Dialogue Popup")]
        public DialoguePopup dialoguePopup;
        
        // 다수의 normal, image 팝업들을 담고 있을 수 있다.
        [Header("Normal Popup & Image Popup")]
        public PopupContainer popupContainer;
        
        // 테스트를 위한 메뉴 버튼들
        [Header("Test Buttons")]
        public Button buttonDialogue;
        public Button buttonToast;
        public Button buttonNormal;
        public Button buttonImage;

        public Sprite m_SpriteTest;
        
        private void Awake()
        {
            dialoguePopup.Init(PopupBaseType.DialoguePopup, "Dialogue", "Content");
            
            popupContainer.Init();
            
            buttonDialogue.onClick.AddListener(OnClickDialogueButton);
            buttonToast.onClick.AddListener(OnClickToastButton);
            buttonNormal.onClick.AddListener(OnClickNormalButton);
            buttonImage.onClick.AddListener(OnClickImagePopupButton);
        }

        private void Start()
        {
            
        }

        public void Init()
        {
            
        }
        
        private void OnClickDialogueButton()
        {
            dialoguePopup.Open();
        }
        
        private void OnClickToastButton()
        {
            popupContainer.Create(PopupBaseType.ToastMessage, "", "Content testing...");
        }
        
        private void OnClickNormalButton()
        {
            PopupBase popup = popupContainer.Create(PopupBaseType.NormalButtonOne, "Normal Popup", "Content");
            popupContainer.SetNormalButtons("OK", OnClickNormalPopupButtonOne, "", null, "", null);
            popupContainer.Open(popup);
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void OnClickNormalPopupButtonOne()
        {
            Debug.Log("Normal Popup Button One Clicked");
        }

        private void OnClickCloseButton()
        {
            Debug.Log("On Close Button Clicked");
            popupContainer.Close();
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void OnClickImagePopupButton()
        {
            PopupBase popup = popupContainer.Create(PopupBaseType.ButtonWithImage, "Image Popup", "Content");
            popupContainer.SetImageButtons("OK", OnClickImagePopupButtonOne, "", null, "", null);
            popupContainer.SetSprite(m_SpriteTest);
            popupContainer.Open(popup);
        }
        
        private void OnClickImagePopupButtonOne()
        {
            Debug.Log("Image Popup Button One Clicked");
        }
    }
}
