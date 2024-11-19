using UnityEngine;

namespace Framework.UI
{
    public enum PopupType
    {
        // PopupMenu를 상속받는 팝업
        SettingsPopup = 0,      // 설정 팝업
        InventoryPopup,     // 인벤토리 팝업
        QuestListPopup,      // 퀘스트 리스트 팝업
        ShopPopup,          // 상점 팝업
    }

    public enum PopupBaseType
    {
        ToastMessage,   // 토스트 메시지 singleton
        
        // PopupBase를 상속받는 팝업
        NormalButtonOne,    // 버튼이 하나만 있는 팝업
        NormalButtonTwo,    // 버튼이 두개만 있는 팝업
        NormalButtonThree,  // 버튼이 세개만 있는 팝업
        ButtonWithImage,   // 버튼과 함께 화면 중앙에 이미지를 표시하는 팝업이다. (NormalPopup을 상속받는 PopupImage 클래스가 있음)
        
        DialoguePopup,      // 대화 팝업
    }
}