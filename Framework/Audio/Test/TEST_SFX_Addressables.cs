using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Framework.Test
{
    public class TEST_SFX_Addressables : MonoBehaviour
    {
        public Transform m_TransformBgm;
        public Transform m_TransformSfx;
        public Transform m_PrefabBgm;
        public Transform m_PrefabSfx;

        public Button m_ButtonLoadBgm;
        public Button m_ButtonUnloadBgm;
        public Button m_ButtonPlayBgm;
        public Button m_ButtonStopBgm;
        
        public Button m_ButtonSfx_0;
        public Button m_ButtonSfx_1;

        public GameObject m_CubeObject;
        
        
        private void Awake()
        {
            DOTween.Init();
            
            m_ButtonLoadBgm.onClick.AddListener(LoadBgm);
            m_ButtonUnloadBgm.onClick.AddListener(UnloadBgm);
            m_ButtonPlayBgm.onClick.AddListener(PlayBgm);
            m_ButtonStopBgm.onClick.AddListener(StopBgm);
            
            m_ButtonSfx_0.onClick.AddListener(PlaySfx_0);
            m_ButtonSfx_1.onClick.AddListener(PlaySfx_1);

            SoundController.Instance.Init(m_TransformBgm, m_TransformSfx, m_PrefabBgm, m_PrefabSfx);
            
            // ȿ�������� �̸� �� �ε��س��´�.
            SoundController.Instance.GetContainer().StartLoadSfxAudioClip("Default", false);
            
            InitCube();
        }

        private void InitCube()
        {
            // �ִϸ��̼�
            AudioSource audioSourceCube = m_CubeObject.GetComponent<AudioSource>();
            audioSourceCube.playOnAwake = false;
            audioSourceCube.loop = true;

            SoundController.Instance.GetContainer().StartLoadBGMAudioClip("bgm_group");
        }

        private void Start()
        {
            
            
        }

        private void LoadBgm()
        {
            SoundController.Instance.GetContainer().StartLoadBGMAudioClip("bgm_group");
        }
        
        private void UnloadBgm()
        {
            SoundController.Instance.GetContainer().UnloadBgmById("00");
        }

        private void PlayBgm()
        {
            SoundController.Instance.PlayBgmById("00");
        }

        private void StopBgm()
        {
            SoundController.Instance.StopBgm();
        }

        private void PlaySfx_0()
        {
            AudioSource audioSourceCube = m_CubeObject.GetComponent<AudioSource>();
            
            SoundController.Instance.PlaySfx(audioSourceCube, "00");
        }
        
        private void PlaySfx_1()
        {
            AudioSource audioSourceCube = m_CubeObject.GetComponent<AudioSource>();
            
            SoundController.Instance.PlaySfx(audioSourceCube, "01");
        }
    }
}
