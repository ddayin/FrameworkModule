using System;
using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;
using UnityEngine.AddressableAssets;

namespace Framework
{
    [Serializable]
    public class SpriteModel
    {
        public string id;
        public string group;
        public string address;
        public AssetReference assetReference;
        public Sprite sprite;

        public SpriteModel(string _id, string _group, string _address, AssetReference _assetReference, Sprite _sprite)
        {
            id = _id;
            group = _group;
            address = _address;
            assetReference = _assetReference;
            sprite = _sprite;
        }
    }
    
    /// <summary>
    /// Sprite를 메모리에 올리거나 해제하는 클래스
    /// </summary>
    public class SpriteLoader : BaseLoader
    {
        /// <summary>
        /// key : id, value : SpriteModel
        /// </summary>
        private Dictionary<string, SpriteModel> m_DictionarySprites = new Dictionary<string, SpriteModel>();
        
        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            
        }

        public void CleanUp()
        {
            
        }
        
        #region Load / Unload from memory
        
        /// <summary>
        /// 모든 Sprite들을 다 로드한다.
        /// </summary>
        /// <returns></returns>
        public bool LoadAllSprites()
        {
            Debug.Log("LoadAllSprites() called");

            foreach (var spriteModel in m_DictionarySprites)
            {
                string groupName = spriteModel.Value.group;
                LoadSpriteByGroup(groupName, null);
            }
            
            return true;
        }

        /// <summary>
        /// 그룹 별로 Sprite를 로드한다.
        /// </summary>
        /// <param name="_group"></param>
        /// <param name="_callback"></param>
        /// <returns></returns>
        public bool LoadSpriteByGroup(string _group, Action<Sprite> _callback)
        {
            foreach (var kvp in m_DictionarySprites)
            {
                if (kvp.Value.group.Equals(_group) == false)
                {
                    continue;
                }
                
                kvp.Value.assetReference.LoadAssetAsync<Sprite>().Completed += (operation) =>
                {
                    kvp.Value.sprite = operation.Result;
                    _callback?.Invoke(kvp.Value.sprite);
                };
            }
            
            return true;
        }

        /// <summary>
        /// id로 Sprite를 로드한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_callback"></param>
        /// <returns></returns>
        public bool LoadSpriteById(string _id, Action<Sprite> _callback)
        {
            Debug.Log($"Starting to load Sprite. id: {_id}");
            
            if (m_DictionarySprites.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }

            if (m_DictionarySprites[_id].sprite == null)
            {
                m_DictionarySprites[_id].assetReference
                    .LoadAssetAsync<Sprite>().Completed += (operation) =>
                {
                    m_DictionarySprites[_id].sprite = operation.Result;
                    _callback?.Invoke(m_DictionarySprites[_id].sprite);
                };
            }
            else
            {
                _callback?.Invoke(m_DictionarySprites[_id].sprite);
            }

            return true;
        }

        /// <summary>
        /// addressables의 address로 Sprite를 로드한다.
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_callback"></param>
        /// <returns></returns>
        public bool LoadSpriteByAddress(string _address, Action<Sprite> _callback)
        {
            Debug.Log($"Starting to load Sprite. address: {_address}");

            foreach (var keyValuePair in m_DictionarySprites)
            {
                if (_address.Equals(keyValuePair.Value.address) == false)
                {
                    Debug.LogError(keyValuePair.Value.address);
                    return false;
                }
                
                if (keyValuePair.Value.sprite == null)
                {
                    keyValuePair.Value.assetReference.LoadAssetAsync<Sprite>().Completed += (operation) =>
                    {
                        keyValuePair.Value.sprite = operation.Result;
                        _callback?.Invoke(keyValuePair.Value.sprite);
                    };
                }
                else
                {
                    _callback?.Invoke(keyValuePair.Value.sprite);
                }
            }
            
            return true;
        }
        
        /// <summary>
        /// 모두 메모리 상에서 해제
        /// </summary>
        /// <returns></returns>
        public bool CleanUpAll()
        {
            Debug.Log("CleanUpAll() called");
            foreach (var keyValuePair in m_DictionarySprites)
            {
                keyValuePair.Value.assetReference.ReleaseAsset();
            }

            m_DictionarySprites.Clear();
            m_DictionarySprites = null;
            
            return true;
        }

        /// <summary>
        /// 그룹 별로 메모리 상에서 해제
        /// </summary>
        /// <param name="_group"></param>
        /// <returns></returns>
        public bool UnloadByGroup(string _group)
        {
            Debug.Log($"Starting to unload Sprite. group: {_group}");
            
            if (m_DictionarySprites.ContainsKey(_group) == false)
            {
                Debug.LogError(_group);
                return false;
            }

            foreach (var keyValuePair in m_DictionarySprites)
            {
                if (keyValuePair.Value.group.Equals(_group) == false)
                {
                    continue;
                }
                
                keyValuePair.Value.assetReference.ReleaseAsset();
            }

            m_DictionarySprites.Clear();
            m_DictionarySprites = null;
            
            return true;
        }

        public bool UnloadById(string _id)
        {
            Debug.Log($"Starting to unload Sprite. id: {_id}");

            if (m_DictionarySprites.ContainsKey(_id) == false)
            {
                Debug.LogError(_id);
                return false;
            }
            
            m_DictionarySprites[_id].assetReference.ReleaseAsset();
            
            return true;
        }
#endregion
    }
}