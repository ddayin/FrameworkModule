using System.Collections.Generic;
using UnityEngine;
using Framework;
using Framework.UI;
using UnityEngine.Events;

namespace Framework.UI
{
    /// <summary>
    ///  PopupNormal, PopupImage 등의 팝업들만 생성하고 관리하는 컨테이너,
    /// 나머지 팝업들은 오로지 하나만 존재하기 때문에 컨테이너가 필요하지 않음
    /// </summary>
    public class PopupContainer : MonoBehaviour
    {
        public GameObject prefabNormalPopup;
        public GameObject prefabImagePopup;
        
        private Stack<PopupBase> popupsStack = new Stack<PopupBase>();  // PopupNormal or PopupImage

        private PopupBase currentPopup;
        
        public Sprite m_SpriteTest;
        
        private void Awake()
        {
            
        }

        public void Init()
        {
            // 지금은 초기화할 것이 없는듯. 그래도 필수로 Init()은 호출해야함
            // Init() => Create() 순서로 생성해야함
        }
        
        /// <summary>
        /// 팝업 Instantiate() 함수로 생성됨
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="sprite"></param>
        public PopupBase Create(PopupBaseType type, string title, string content, Sprite sprite = null, bool _hasCloseButton = true)
        {
            PopupBase popup = null;
            
            switch (type)
            {
                case PopupBaseType.NormalButtonOne:
                case PopupBaseType.NormalButtonTwo:
                case PopupBaseType.NormalButtonThree:
                    popup = InitNormalPopup(type, title, content, _hasCloseButton);
                    return popup;
                
                case PopupBaseType.ButtonWithImage:
                    popup = InitImagePopup(type, title, content, sprite, _hasCloseButton);
                    return popup;
                
                case PopupBaseType.ToastMessage:
                    ToastMessage.Instance.Open();
                    ToastMessage.Instance.Init(content);
                    break;
                
                case PopupBaseType.DialoguePopup:
                    break;
                /*
                case PopupType.SettingsPopup:
                case PopupType.InventoryPopup:
                case PopupType.QuestListPopup:
                case PopupType.ShopPopup:
                    Debug.LogError(type.ToString());
                    break;
                */
                
                default:
                    Debug.LogError(type.ToString());
                    break;
            }

            return null;
        }

        /// <summary>
        /// 팝업 타입과 여러가지 파라미터를 입력해서 팝업을 생성하고 스택에 쌓는다.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private PopupBase InitNormalPopup(PopupBaseType type, string title, string content, bool _hasCloseButton = true)
        {
            GameObject newPopup = CommonUtils.Instantiate(prefabNormalPopup, this.transform);
            PopupNormal popupNormal = newPopup.transform.GetComponent<PopupNormal>();
            popupNormal.Init(type, title, content, _hasCloseButton);
            // popupNormal.SetButtons("FIRST", () => { Debug.Log("FIRST"); }, null, null, null, null);
            // popupNormal.SetCloseButton(Close);
            popupNormal.SetStackedPosition(popupsStack.Count);
            currentPopup = popupNormal;
            popupsStack.Push(popupNormal);
            Debug.Log("popupsStack.Count = " + popupsStack.Count);

            return popupNormal;
        }
        
        /// <summary>
        /// 설정 필수
        /// </summary>
        /// <param name="first"></param>
        /// <param name="click_1"></param>
        /// <param name="second"></param>
        /// <param name="click_2"></param>
        /// <param name="third"></param>
        /// <param name="click_3"></param>
        public void SetNormalButtons(string first, UnityAction click_1, string second, UnityAction click_2, string third, UnityAction click_3)
        {
            currentPopup.SetButtons(first, click_1, second, click_2, third, click_3);
        }
        
        /// <summary>
        /// 설정 필수
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        private PopupBase InitImagePopup(PopupBaseType type, string title, string content, Sprite _sprite, bool _hasCloseButton = true)
        {
            GameObject newPopup = CommonUtils.Instantiate(prefabImagePopup, this.transform);
            PopupImage popupImage = newPopup.transform.GetComponent<PopupImage>();
            popupImage.Init(type, title, content, _hasCloseButton);
            popupImage.SetSprite(_sprite);
            popupImage.SetStackedPosition(popupsStack.Count);
            currentPopup = popupImage;
            popupsStack.Push(popupImage);
            Debug.Log("popupsStack.Count = " + popupsStack.Count);

            return popupImage;
        }
        
        /// <summary>
        /// 설정 필수
        /// </summary>
        /// <param name="sprite"></param>
        public void SetSprite(Sprite sprite)
        {
            // popupsStack.Peek().SetSprite(sprite);
            currentPopup.SetSprite(sprite);
        }

        /// <summary>
        /// 설정 필수
        /// </summary>
        /// <param name="first"></param>
        /// <param name="click_1"></param>
        /// <param name="second"></param>
        /// <param name="click_2"></param>
        /// <param name="third"></param>
        /// <param name="click_3"></param>
        public void SetImageButtons(string first, UnityAction click_1, string second, UnityAction click_2, string third, UnityAction click_3)
        {
            // popupsStack.Peek().SetButtons(first, click_1, null, null, null, null);
            currentPopup.SetButtons(first, click_1, null, null, null, null);
        }
        
        public void Add(PopupBase popup)
        {
            popupsStack.Push(popup);
            currentPopup = popup;
        }

        public void Remove()
        {
            PopupBase popup = popupsStack.Pop();
            Destroy(popup.gameObject);
            currentPopup = popupsStack.Pop();
        }

        public void Open(PopupBase _popup)
        {
            popupsStack.Push(_popup);
            currentPopup = _popup;
            currentPopup.Open();
        }

        /*
        private void Close()
        {
            PopupBase popup = popupsStack.Peek();
            popup.Close();
            // popupsStack.Pop();
            currentPopup = popup;
        }
        */
        
        public void Close()
        {
            PopupBase popup = popupsStack.Peek();
            popup.OnClickCloseButton();
            popupsStack.Pop();
            currentPopup = popupsStack.Peek();
        }
    }
}
