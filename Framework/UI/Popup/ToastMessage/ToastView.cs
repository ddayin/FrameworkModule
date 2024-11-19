using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Framework.UI
{
    public class ToastView : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI m_Text;
        public Image m_Icon;
        
        private void Awake()
        {
            if (m_Text == null) m_Text = GetComponentInChildren<TextMeshProUGUI>();
            if (m_Icon == null) m_Icon = GetComponentInChildren<Image>();
        }

        public void Init(string _text, Sprite _icon = null)
        {
            m_Text.text = _text;
            if (_icon != null) m_Icon.sprite = _icon;
            else m_Icon.gameObject.SetActive(false);
        }
        
        public void SetPosition(ToastPosition _position)
        {
            
            RectTransform rt = transform as RectTransform;
            
            
            switch (_position)
            {
                case ToastPosition.Center:
                    rt.anchorMax = new Vector2(0.5f, 0.5f);
                    rt.anchorMin = new Vector2(0.5f, 0.5f);
                    rt.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case ToastPosition.Top:
                    //Screen.height;
                        
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
    }
}