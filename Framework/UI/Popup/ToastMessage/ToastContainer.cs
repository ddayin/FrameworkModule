using System.Collections.Generic;
using UnityCommunity.UnitySingleton;
using UnityEngine;

namespace Framework.UI
{
    public enum ToastPosition
    {
        Center = 0,
        Top,
        Bottom,
        Right,
        Left
    }
    
    public class ToastContainer : PersistentMonoSingleton<ToastContainer>
    {
        private Queue<ToastView> m_ToastQueue = new Queue<ToastView>();
        public GameObject m_ToastPrefab;
        
        protected override void Awake()
        {
            base.Awake();
        }
        
        public void AddToast(string _text, Sprite _icon = null)
        {
            ToastView newToast = InstantiateToast();
            
            newToast.Init(_text, _icon);
            
            m_ToastQueue.Enqueue(newToast);
            if (m_ToastQueue.Count == 1)
            {
                ShowToast();
            }
        }

        private ToastView InstantiateToast()
        {
            GameObject newToast = Instantiate(m_ToastPrefab, Vector3.zero, Quaternion.identity, transform);
            ToastView toast = newToast.GetComponent<ToastView>();
            
            return toast;
        }

        private void SetPosition(ToastPosition _position)
        {
            switch (_position)
            {
                case ToastPosition.Center:
                    break;
                case ToastPosition.Top:
                    break;
                case ToastPosition.Bottom:
                    break;
                case ToastPosition.Right:
                    break;
                case ToastPosition.Left:
                    break;
                default:
                    break;
            }
        }

        public void ShowToast()
        {
            ToastView toast = m_ToastQueue.Dequeue();
            toast.gameObject.SetActive(true);
        }
    }
}