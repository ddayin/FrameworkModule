using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Redcode.Pools;

namespace Framework
{
    [Serializable]
    public class SoundModel
    {
        public string id;
        public string group;        // 그룹 별로 메모리 관리를 하기 위함
        public string address;      // addressable address을 사용하여 AudioClip을 로드하기 위함
        public AssetReference assetReference;
        public AudioSource audioSource;
        public AudioClip audioClip;
    }

    /// <summary>
    /// 사운드 데이터들을 관리하는 컨테이너
    /// </summary>
    public class SoundContainer : MonoBehaviour
    {
        /// <summary>
        /// 배경음 그룹 별로 SoundModel을 관리하는 Dictionary
        /// key : id, value : SoundModel
        /// </summary>
        private Dictionary<string, SoundModel> m_DictionaryBgm = new Dictionary<string, SoundModel>();

        /// <summary>
        /// 효과음 그룹 별로 SoundModel을 관리하는 Dictionary
        /// key : id, value : SoundModel
        /// </summary>
        private Dictionary<string, SoundModel> m_DictionarySfx = new Dictionary<string, SoundModel>();

        private Transform m_ParentOfBgm;
        private Transform m_ParentOfSfx;
        
        [Header("배경음 프리팹 인스펙터 상에서 설정 필수")]
        public Transform m_BgmPrefab;

        [Header("효과음 프리팹 인스펙터 상에서 설정 필수")]
        public Transform m_SfxPrefab;
        
        private void Awake()
        {

        }

        /// <summary>
        /// 초기화 필수 세팅
        /// </summary>
        /// <param name="_bgm"></param>
        /// <param name="_sfx"></param>
        public void Init(Transform _bgm, Transform _sfx, Transform _prefabBgm, Transform _prefabSfx)
        {
            m_ParentOfBgm = _bgm;
            m_ParentOfSfx = _sfx;
            m_BgmPrefab = _prefabBgm;
            m_SfxPrefab = _prefabSfx;
        }

        public AudioSource GetBgmAudioSource(string _id)
        {
            if (m_DictionaryBgm.TryGetValue(_id, out SoundModel soundModel) == false)
            {
                Debug.LogError(_id);
                return null;
            }

            if (soundModel.audioSource == null)
            {
                Debug.LogError(_id);
                return null;
            }
            else
            {
                return soundModel.audioSource;
            }
        }

        public AudioSource GetSfxAudioSource(string _id)
        {
            if (m_DictionarySfx.TryGetValue(_id, out SoundModel soundModel) == false)
            {
                Debug.LogError(_id);
                return null;
            }

            if (soundModel.audioSource == null)
            {
                Debug.LogError(_id);
                return null;
            }
            else
            {
                return soundModel.audioSource;
            }
        }

        public SoundModel GetBgmById(string _group, string _address, string _id)
        {
            if (m_DictionaryBgm.Count == 0)
            {
                Debug.LogWarning("m_DictionaryBgm is empty");
                StartLoadBgm(_group, _address, _id);
            }

            if (m_DictionaryBgm.ContainsKey(_id) == true)
            {
                return m_DictionaryBgm[_id];
            }
            else
            {
                Debug.LogError(_id);
                return null;
            }
        }

        public Dictionary<string, SoundModel> GetBgm()
        {
            if (m_DictionaryBgm.Count == 0)
            {
                Debug.LogError("m_DictionaryBgm is empty");
                return null;
            }
            else
            {
                return m_DictionaryBgm;    
            }
        }
        
        public Dictionary<string, SoundModel> GetSfx()
        {
            if (m_DictionarySfx.Count == 0)
            {
                Debug.LogError("m_DictionarySfx is empty");
                return null;
            }
            else
            {
                return m_DictionarySfx;    
            }
        }
        
        public void StartLoadBgm(string _group, string _address, string _id)
        {
            StartCoroutine(LoadBgmCoroutine(_group, _address, _id));
        }
        
        /// <summary>
        /// 그룹 별로 배경음을 메모리 상에 적재한다. 세 개의 인자를 모두 넘겨주어야 한다.
        /// </summary>
        /// <param name="_group"></param>
        public void StartLoadBGMAudioClip(string _group)
        {
            StartCoroutine(LoadBGMAudioClipCoroutine(_group));
        }
        
        private IEnumerator LoadBgmCoroutine(string _group, string _address, string _id)
        {
            Debug.Log($"Start to load BGM AudioClip. id: {_id}");
            
            if (m_DictionaryBgm.ContainsKey(_id) == true)
            {
                Debug.LogWarning("Already loaded " + _id);
                yield break;
            }
            
            m_DictionaryBgm.Add(_id, new SoundModel
            {
                id = _id,
                group = _group,
                address = _address,
                assetReference = new AssetReference(_address),
                audioSource = null,
                audioClip = null
            });
            
            var handle = m_DictionaryBgm[_id].assetReference.LoadAssetAsync<AudioClip>();
            yield return handle;
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //GameObject newBgmObject = Instantiate(m_BgmPrefab, Vector3.zero, Quaternion.identity, m_ParentOfBgm);
                var pool = Pool.Create<Transform>(m_BgmPrefab, FrameworkDefine.POOL_BGM_MAX, m_ParentOfBgm);
                Transform newBgm = pool.Get();
                AudioSource audioSource = newBgm.GetComponent<AudioSource>();
                if (audioSource == null) audioSource = newBgm.gameObject.AddComponent<AudioSource>();
                audioSource.clip = handle.Result;

                m_DictionaryBgm[_id].audioSource = audioSource;
                m_DictionaryBgm[_id].audioClip = handle.Result;
                
                Debug.Log($"Completed to load BGM AudioClip. id: {_id} address: {_address} group: {_group}");
            }
            else
            {
                Debug.LogError(handle.Status.ToString());
            }
        }
        
        /// <summary>
        /// [중요] Addressables를 이용하여 배경음을 로드한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <returns></returns>
        private IEnumerator LoadBGMAudioClipCoroutine(string _group = "Default")
        {
            Debug.Log($"Start to load BGM AudioClip. group: {_group}");
            
            foreach (var keyValuePair in m_DictionaryBgm)
            {
                if (keyValuePair.Key.Equals(_group) == true)
                {
                    Debug.LogWarning("Already loaded " + _group);
                    yield break;
                }
                
                m_DictionaryBgm.Add(keyValuePair.Key, new SoundModel
                {
                    id = keyValuePair.Key,
                    group = _group,
                    address = keyValuePair.Value.address,
                    assetReference = new AssetReference(keyValuePair.Value.address),
                    audioSource = null,
                    audioClip = null
                });
                
                var handle = m_DictionaryBgm[keyValuePair.Key].assetReference.LoadAssetAsync<AudioClip>();
                yield return handle;
                
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    // Synchronous Instantiation: However, the instantiation itself is executed synchronously.
                    //GameObject newBgmObject = Instantiate(m_BgmPrefab, Vector3.zero, Quaternion.identity, m_ParentOfBgm);
                    var pool = Pool.Create<Transform>(m_BgmPrefab, FrameworkDefine.POOL_BGM_MAX, m_ParentOfBgm);
                    Transform newBgm = pool.Get();
                    AudioSource audioSource = newBgm.GetComponent<AudioSource>();
                    if (audioSource == null) audioSource = newBgm.gameObject.AddComponent<AudioSource>();
                    audioSource.clip = handle.Result;

                    m_DictionaryBgm[keyValuePair.Key].audioSource = audioSource;
                    m_DictionaryBgm[keyValuePair.Key].audioClip = handle.Result;
                
                    Debug.Log($"Completed to load BGM AudioClip. id: {keyValuePair.Key} address: {keyValuePair.Value.address} group: {_group}");
                }
                else
                {
                    Debug.LogError(handle.Status.ToString());
                }
            }
        }
        
        /// <summary>
        /// 그룹 별로 효과음을 메모리 상에 적재한다. 네 개의 인자를 모두 넘겨주어야 한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_is3Dobject"></param>
        public void StartLoadSfxAudioClip(string _group = "Default", bool _is3Dobject = false)
        {
            StartCoroutine(LoadSfxAudioClipCoroutine(_group, _is3Dobject));
        }
        
        /// <summary>
        /// [중요] Addressables를 이용하여 효과음을 로드한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_is3Dobject"></param>
        /// <returns></returns>
        private IEnumerator LoadSfxAudioClipCoroutine(string _group, bool _is3Dobject = false)
        {
            Debug.Log($"Start to load SFX AudioClip. group: {_group} is3Dobject: {_is3Dobject}");

            foreach (var keyValuePair in m_DictionarySfx)
            {
                if (keyValuePair.Key.Equals(_group) == true)
                {
                    Debug.LogWarning("Already loaded " + _group);
                    yield break;
                }

                m_DictionarySfx.Add(keyValuePair.Key, new SoundModel
                {
                    id = keyValuePair.Key,
                    group = _group,
                    address = keyValuePair.Value.address,
                    assetReference = new AssetReference(keyValuePair.Value.address),
                    audioSource = null,
                    audioClip = null
                });

                var handle = m_DictionarySfx[keyValuePair.Key].assetReference.LoadAssetAsync<AudioClip>();
                yield return handle;
            
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var pool = Pool.Create<Transform>(m_SfxPrefab, FrameworkDefine.POOL_SFX_MAX, m_ParentOfBgm);
                    Transform newSfx = pool.Get();
                    AudioSource audioSource = newSfx.GetComponent<AudioSource>();
                    if (audioSource == null) audioSource = newSfx.gameObject.AddComponent<AudioSource>();
                    audioSource.clip = handle.Result;

                    if (_is3Dobject == true)
                    {
                        // Set the 3D properties of the audio source
                        SetAudioSource3DProperties(audioSource, 50f); // Use a max distance of 50 units    
                    }

                    m_DictionarySfx[keyValuePair.Key].audioSource = audioSource;
                    m_DictionarySfx[keyValuePair.Key].audioClip = handle.Result;
                
                    Debug.Log($"Completed to load SFX AudioClip. id: {keyValuePair.Key} address: {keyValuePair.Value.address} group: {_group} is3Dobject: {_is3Dobject}");
                }
                else
                {
                    Debug.LogError(handle.Status.ToString());
                }
            }
        }
        
        /// <summary>
        /// [필수] 3D 오브젝트가 많기 때문에 필수로 설정해야 함
        /// 만약 3D 오브젝트가 아니라 UI 버튼 클릭과 같은 효과음이라면 3D 속성을 설정하지 않아도 됨
        /// </summary>
        /// <param name="_audioSource"></param>
        /// <param name="_maxDistance"></param>
        private void SetAudioSource3DProperties(AudioSource _audioSource, float _maxDistance)
        {
            // Make the audio source purely 3D
            _audioSource.spatialBlend = 1.0f;
            // Use logarithmic rolloff, which is more natural sounding
            _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            // Set the maximum distance at which the audio source will make a sound
            _audioSource.maxDistance = _maxDistance;
        }

        public void CleanUp()
        {
            
        }
        
        /// <summary>
        /// 메모리 상에서 모두 해제, 씬 전환될 때!
        /// </summary>
        public void CleanUpAll()
        {   
            Debug.Log("CleanUpAll() called");
            
            UnloadBgm();
            UnloadSfx();
        }
        
        /// <summary>
        /// 모든 배경음을 메모리 상에서 해제한다.
        /// </summary>
        public void UnloadBgm()
        {
            Debug.Log("CleanUpBgm() called");
            
            foreach (var keyValuePair in m_DictionaryBgm)
            {
                keyValuePair.Value.assetReference.ReleaseAsset();
                Destroy(keyValuePair.Value.audioSource.gameObject);
            }
            m_DictionaryBgm.Clear();
            m_DictionaryBgm = null;
        }

        /// <summary>
        /// 모든 효과음을 메모리 상에서 해제한다.
        /// </summary>
        public void UnloadSfx()
        {
            Debug.Log("CleanUpSfx() called");
            
            foreach (var keyValuePair in m_DictionarySfx)
            {
                keyValuePair.Value.assetReference.ReleaseAsset();
                Destroy(keyValuePair.Value.audioSource.gameObject);
            }
            m_DictionarySfx.Clear();
            m_DictionarySfx = null;
        }

        public void UnloadAll()
        {
            UnloadBgm();
            UnloadSfx();
        }
        
        /// <summary>
        /// 그룹 별로 메모리 상에서 모두 해제
        /// </summary>
        /// <param name="_group"></param>
        public void UnloadBgmByGroup(string _group)
        {   
            Debug.Log("CleanUpBgmByGroup() called. group: {_group}");
            
            foreach (var keyValuePair in m_DictionaryBgm)
            {
                if (keyValuePair.Value.group.Equals(_group) == false)
                {
                    continue;
                }
                keyValuePair.Value.assetReference.ReleaseAsset();
                Destroy(keyValuePair.Value.audioSource.gameObject);
            }
            
            m_DictionaryBgm.Remove(_group);
        }
        
        /// <summary>
        /// 효과음 그룹 별로 메모리 상에서 모두 해제
        /// </summary>
        /// <param name="_group"></param>
        public void UnloadSfxByGroup(string _group)
        {
            Debug.Log("CleanUpSfxByGroup() called. group: {_group}");
            
            foreach (var keyValuePair in m_DictionarySfx)
            {
                if (keyValuePair.Value.group.Equals(_group) == false)
                {
                    continue;
                }
                keyValuePair.Value.assetReference.ReleaseAsset();
                Destroy(keyValuePair.Value.audioSource.gameObject);
            }
            
            m_DictionarySfx.Remove(_group);
        }
        
        /// <summary>
        /// id 별로 배경음을 메모리 상에서 해제한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool UnloadBgmById(string _id)
        {
            if (m_DictionaryBgm.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            if (m_DictionaryBgm.ContainsKey(_id) == true)
            {
                m_DictionaryBgm[_id].assetReference.ReleaseAsset();
                Destroy(m_DictionaryBgm[_id].audioSource.gameObject);
                return true;
            }
            else
            {
                Debug.LogError(_id);
                return false;
            }
        }
        
        /// <summary>
        /// address 별로 배경음을 메모리 상에서 해제한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        public bool UnloadBgmByAddress(string _address)
        {
            foreach (var keyValuePair in m_DictionaryBgm)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    keyValuePair.Value.assetReference.ReleaseAsset();
                    Destroy(keyValuePair.Value.audioSource.gameObject);
                    return true;
                }
            }

            Debug.LogError(_address);
            return false;
        }

        /// <summary>
        /// id 별로 효과음을 메모리 상에서 해제한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool UnloadSfxById(string _id)
        {
            if (m_DictionarySfx.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            if (m_DictionarySfx.ContainsKey(_id) == true)
            {
                m_DictionarySfx[_id].assetReference.ReleaseAsset();
                Destroy(m_DictionarySfx[_id].audioSource.gameObject);
                return true;
            }
            else
            {
                Debug.LogError(_id);
                return false;
            }
        }
        
        /// <summary>
        /// address 별로 효과음을 메모리 상에서 해제한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <returns></returns>
        public bool UnloadSfxByAddress(string _address)
        {
            foreach (var keyValuePair in m_DictionarySfx)
            {
                if (keyValuePair.Value.address.Equals(_address) == true)
                {
                    keyValuePair.Value.assetReference.ReleaseAsset();
                    Destroy(keyValuePair.Value.audioSource.gameObject);
                    return true;
                }
            }

            Debug.LogError(_address);
            return false;
        }

        
    }
}
