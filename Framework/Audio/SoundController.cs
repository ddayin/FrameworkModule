using UnityCommunity.UnitySingleton;
using UnityEngine;

namespace Framework
{
    /// <summary>
    ///  배경음, 효과음을 포함한 사운드 제어
    /// Addressables에서 많은 기능을 제공하는데 이 SoundController에서는 가장 기본적인 AssetReference를 이용한다.
    /// TODO: AssetReference가 아닌 폴더 별로 배경음, 효과음을 관리하는 방법을 찾아보자. 아래 링크의 샘플 코드 참고할 것 => Addressables.InstantiateAsync() 사용
    /// https://docs.unity3d.com/Packages/com.unity.addressables@1.21/manual/load-addressable-assets.html
    /// TODO: 효과음이 여러 개일 때, 동시에 여러 개 재생이 가능하도록 수정해야 한다. => 수정은 했으나, 볼륨이 낮아지는 이슈 존재 (볼륨 조정을 안 했고, 어떤 옵션값 조정으로 자동으로 볼륨 조정되는 것을 막을 수 있음
    /// TODO: pool로 미리 필요한 효과음들을 불러이들이고 SetActive()로 제어하도록 개선해야 한다.
    /// TODO: 배경음의 경우, 배경음 하나가 끝나면, 다음 배경음이 바로 재생되도록 개선해야 한다.
    /// TODO: 효과음 그룹을 지정해 선택한 그룹의 효과음들만 불러들이도록 개선해야 한다. => 완료
    /// TODO: 어떻게 보면 배경음과 효과음은 코드 상으로 구분이 될 뿐이지 내용은 동일하다. 이 둘을 통합할 수 있는 방법을 찾아서 중복된 코드를 줄여보자.
    /// </summary>
    [RequireComponent(typeof(SoundContainer))]
    public class SoundController : PersistentMonoSingleton<SoundController>
    {
        private SoundContainer m_Container;
        
        #region Mono Events
        protected override void Awake()
        {
            base.Awake();
            
            m_Container = GetComponent<SoundContainer>();
        }
        #endregion


        public void Init(Transform _parentOfBgm, Transform _parentOfSfx, Transform _prefabBgm, Transform _prefabSfx)
        {
            m_Container.Init(_parentOfBgm, _parentOfSfx, _prefabBgm, _prefabSfx);
        }

        public void CleanUp()
        {
            
        }

        public SoundContainer GetContainer()
        {
            return m_Container;
        }
        
        /// <summary>
        /// id의 배경음을 재생한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool PlayBgmById(string _id)
        {
            Debug.Log("Play BGM by {_id}");

            var bgm = m_Container.GetBgm();
            if (bgm.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            else
            {
                bgm[_id].audioSource.Play();
                return true;
            }
        }

        /// <summary>
        /// 그룹의 지정된 address의 배경음을 재생한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_address"></param>
        /// <returns></returns>
        public bool PlayBgmByAddress(string _address)
        {
            Debug.Log("Play BGM by {_address}");
            
            var bgm = m_Container.GetBgm();
            
            foreach (var keyValuePair in bgm)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    keyValuePair.Value.audioSource.Play();
                    return true;
                }
            }

            Debug.LogError(_address);
            return false;
        }
        
        /// <summary>
        /// 모든 배경음들의 재생을 정지한다.
        /// </summary>
        /// <returns></returns>
        public bool StopBgm()
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            foreach (var keyValuePair in bgm)
            {
                keyValuePair.Value.audioSource.Stop();
            }
            
            return true;
        }
        
        /// <summary>
        /// id 별로 배경음을 정지한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool StopBgmById(string _id)
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            if (bgm.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            else
            {
                bgm[_id].audioSource.Stop();
                return true;
            }
        }
        
        /// <summary>
        /// address 별로 배경음을 정지한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_address"></param>
        /// <returns></returns>
        public bool StopBgmByAddress(string _address)
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            foreach (var keyValuePair in bgm)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    keyValuePair.Value.audioSource.Stop();
                    return true;
                }
            }
            
            Debug.LogError(_address);
            return false;
        }

        /// <summary>
        /// 모든 배경음들을 일시정지한다.
        /// </summary>
        /// <returns></returns>
        public bool PauseBGMAll()
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            foreach (var keyValuePair in bgm)
            {
                keyValuePair.Value.audioSource.Pause();
            }

            return true;
        }
        
        /// <summary>
        /// 그룹의 지정된 id의 배경음을 일시정지한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool PauseBgmById(string _id)
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            if (bgm.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            bgm[_id].audioSource.Pause();
            return true;
        }
        
        /// <summary>
        /// 모든 배경음들을 일시정지 해제한다.
        /// </summary>
        /// <returns></returns>
        public bool UnPauseBgmAll()
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.Count == 0)
            {
                Debug.LogWarning("No BGM loaded");
                return false;
            }
            
            foreach (var keyValuePair in bgm)
            {
                keyValuePair.Value.audioSource.UnPause();
            }
            return true;
        }

        /// <summary>
        /// 지정된 id의 배경음을 일시정지 해제한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool UnPauseBgmById(string _id)
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            bgm[_id].audioSource.UnPause();
            return true;
        }
        
        /// <summary>
        /// 그룹의 지정된 id의 효과음을 재생한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool PlaySfx(string _id)
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            else
            {
                sfx[_id].audioSource.Play();
                return true;
            }
        }

        /// <summary>
        /// _audioSource를 이용하여 효과음을 재생한다.
        /// 3D 효과음에 적합하다
        /// </summary>
        /// <param name="_audioSource">AudioSource는 다른 오브젝트의 AudioSource를 가져온다.</param>
        /// <param name="_group"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool PlaySfx(AudioSource _audioSource, string _id)
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            if (sfx[_id].audioSource == null)
            {
                sfx[_id].audioSource = _audioSource;
            }
            else
            {
                Debug.LogError(_id);
                return false;
            }

            sfx[_id].audioSource.PlayOneShot(sfx[_id].audioClip);
            return true;
        }

        /// <summary>
        /// 지정된 address의 효과음을 재생한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        public bool PlaySfxByAddress(string _address)
        {
            var sfx = m_Container.GetSfx();
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    if (keyValuePair.Value.audioSource == null)
                    {
                        // TODO: audioSource를 생성하는 방법을 찾아보자
                        Debug.LogError(_address);
                        return false;
                    }
                    else
                    {
                        keyValuePair.Value.audioSource.Play();
                        return true;    
                    }
                }
            }

            Debug.LogError(_address);
            return false;
        }
        
        /// <summary>
        /// 모든 효과음 재생을 정지한다.
        /// </summary>
        /// <returns></returns>
        public bool StopSfxAll()
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.Count == 0)
            {
                Debug.LogWarning("No SFX loaded");
                return false;
            }
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.audioSource == null)
                {
                    Debug.LogError(keyValuePair.Key);
                    continue;
                }
                keyValuePair.Value.audioSource.Stop();
            }
            
            return true;
        }

        /// <summary>
        /// 지정된 id의 효과음을 정지한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool StopSfx(string _id)
        {
            var sfx = m_Container.GetSfx();
            if (sfx.Count == 0)
            {
                Debug.LogWarning("No SFX loaded");
                return false;
            }
            
            if (sfx.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            else
            {
                sfx[_id].audioSource.Stop();
                return true;
            }
        }
        
        /// <summary>
        /// 모든 효과음들을 일시정지한다.
        /// </summary>
        /// <returns></returns>
        public bool PauseSfxAll()
        {
            var sfx = m_Container.GetSfx();
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.audioSource == null)
                {
                    Debug.LogError(keyValuePair.Key);
                    continue;
                }
                keyValuePair.Value.audioSource.Pause();
            }

            return true;
        }
        
        /// <summary>
        /// 지정된 id의 효과음을 일시정지한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool PauseSfx(string _id)
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.ContainsKey(_id) == true)
            {
                if (sfx[_id].audioSource == null)
                {
                    Debug.LogError(_id);
                    return false;
                }
                
                sfx[_id].audioSource.Pause();
                return true;
            }
            else
            {
                Debug.LogError(_id);
                return false;
            }
        }
        
        /// <summary>
        /// 모든 효과음들을 일시정지 해제한다.
        /// </summary>
        /// <returns></returns>
        public bool UnPauseSfxAll()
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.Count == 0)
            {
                Debug.LogWarning("No SFX loaded");
                return false;
            }
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.audioSource == null)
                {
                    Debug.LogError(keyValuePair.Key);
                    continue;
                }
                keyValuePair.Value.audioSource.UnPause();
            }

            return true;
        }
        
        /// <summary>
        /// 지정된 id의 효과음을 일시정지 해제한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool UnPauseSfx(string _id)
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.ContainsKey(_id) == true)
            {
                if (sfx[_id].audioSource == null)
                {
                    Debug.LogError(_id);
                    return false;
                }
                sfx[_id].audioSource.UnPause();
                return true;
            }
            else
            {
                Debug.LogError(_id);
                return false;
            }
        }
        
        /// <summary>
        /// 배경음 볼륨을 일괄 조정한다.
        /// </summary>
        /// <param name="_volume"></param>
        public void SetBgmVolume(float _volume)
        {
            var bgm = m_Container.GetBgm();
            
            foreach (var keyValuePair in bgm)
            {
                if (keyValuePair.Value.audioSource == null)
                {
                    Debug.LogError(keyValuePair.Key);
                    continue;
                }
                
                keyValuePair.Value.audioSource.volume = _volume;
            }
            
            // TODO: PlayerPrefs 사용하지 않도록 개선
            PlayerPrefs.SetFloat("BGMVolume", _volume);
        }

        /// <summary>
        /// 배경음 볼륨 값을 반환한다.
        /// </summary>
        /// <returns></returns>
        public float GetBgmVolume()
        {
            // TODO: PlayerPrefs 사용하지 않도록 개선
            return PlayerPrefs.GetFloat("BGMVolume", 1f);
        }
        
        /// <summary>
        /// 효과음 볼륨을 일괄 조정한다.
        /// </summary>
        /// <param name="_volume"></param>
        public void SetSfxVolume(float _volume)
        {
            var sfx = m_Container.GetSfx();
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.audioSource == null)
                {
                    Debug.LogError(keyValuePair.Key);
                    continue;
                }
                
                keyValuePair.Value.audioSource.volume = _volume;
            }
            
            // TODO: PlayerPrefs 사용하지 않도록 개선
            PlayerPrefs.SetFloat("SfxVolume", _volume);
        }
        
        /// <summary>
        /// 효과음 볼륨 값을 반환한다.
        /// </summary>
        /// <returns></returns>
        public float GetSfxVolume()
        {
            // TODO: PlayerPrefs 사용하지 않도록 개선
            return PlayerPrefs.GetFloat("SfxVolume", 1f);
        }
        
        /// <summary>
        /// 지정된 address를 이용하여 배경음의 id를 반환한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        public string GetBgmId(string _address)
        {
            var bgm = m_Container.GetBgm();
            
            foreach (var keyValuePair in bgm)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    return keyValuePair.Key;
                }
            }
            
            Debug.LogError(_address);
            return null;
        }

        /// <summary>
        /// id를 이용하여 배경음의 address를 반환한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetBgmAddress(string _id)
        {
            var bgm = m_Container.GetBgm();
            
            if (bgm.ContainsKey(_id) == true)
            {
                return bgm[_id].address;
            }
            else
            {
                Debug.LogError(_id);
                return null;
            }
        }
        
        /// <summary>
        /// address를 이용하여 효과음의 id를 반환한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        public string GetSfxId(string _address)
        {
            var sfx = m_Container.GetSfx();
            
            if (sfx.Count == 0)
            {
                Debug.LogWarning("No SFX loaded");
                return null;
            }
            
            foreach (var keyValuePair in sfx)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    return keyValuePair.Key;
                }
            }

            Debug.LogError(_address);
            return null;
        }
        
        /// <summary>
        /// id를 이용하여 효과음의 address를 반환한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetSfxAddress(string _id)
        {
            var sfx = m_Container.GetSfx();
            if (sfx.Count == 0)
            {
                Debug.LogWarning("No SFX loaded");
                return null;
            }
            
            if (sfx.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return null;
            }
            else
            {
                if (sfx.ContainsKey(_id) == true)
                {
                    return sfx[_id].address;
                }
                else
                {
                    Debug.LogError(_id);
                    return null;
                }
            }
        }
    }
}
