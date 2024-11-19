using System;
using UnityEngine;
using DG.Tweening;

namespace Framework.UI
{
    /// <summary>
    ///  팝업 애니메이션이 필요하므로 팝업이 열릴 때와 팝업이 닫힐 때 이펙트가 재생된다.
    /// 실제로 이대로 애니메이션이 쓰일 것은 아니라서, 샘플 코드로 참고만
    /// </summary>
    public class UI_AnimationPopup : MonoBehaviour
    {
        public float duration = 0.5f;
        
        private void Awake()
        {
            
        }
        
        public void OnOpenPopup()
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1f), duration * 0.5f).SetEase(Ease.OutBack).OnComplete(OnCompleteOpen);
        }
        
        private void OnCompleteOpen()
        {
            transform.DOScale(Vector3.one, duration * 0.5f).SetEase(Ease.InBack);
        }
        
        public void OnClosePopup()
        {
            transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
        }
    }
}
