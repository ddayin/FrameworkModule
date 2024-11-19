using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCommunity.UnitySingleton;
using UnityEngine.AddressableAssets;

namespace Framework
{
    [Serializable]
    public class TextureModel
    {
        public string id;
        public string group;
        public string address;
        public AssetReference assetReference;
        public Texture2D texture;

        public TextureModel(string _id, string _group, string _address, AssetReference _assetReference,
            Texture2D _texture)
        {
            id = _id;
            group = _group;
            address = _address;
            assetReference = _assetReference;
            texture = _texture;
        }
    }
    
    /// <summary>
    /// 텍스쳐를 로드하고 관리하는 클래스 (쓸 일이 거의 없을듯)
    /// reference : https://blog.unity.com/engine-platform/accessing-texture-data-efficiently
    /// </summary>
    public class TexturesLoader : BaseLoader
    {
        /// <summary>
        /// key: id, value: TextureModel
        /// </summary>
        private Dictionary<string, TextureModel> m_DictionaryTextureModels =
            new Dictionary<string, TextureModel>();

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

        public Texture2D GetTextureById(string _id)
        {
            if (m_DictionaryTextureModels.TryGetValue(_id, out var textureModel) == false)
            {
                Debug.LogError(_id);
                return null;
            }
            
            if (textureModel.texture != null)
            {
                return textureModel.texture;
            }
            else
            {
                Debug.LogError("TexturesLoader.GetTextureById() : texture is null");
                return null;
            }
        }
        

        public TextureModel GetTextureModelById(string _id)
        {
            if (m_DictionaryTextureModels.TryGetValue(_id, out var textureModel) == false)
            {
                Debug.LogError(_id);
                return null;
            }
            else
            {
                return textureModel;
            }
        }

        public void LoadTexture(string _id, Action<Texture2D> _callback)
        {
            if (m_DictionaryTextureModels.ContainsKey(_id) == true)
            {
                if (m_DictionaryTextureModels[_id].texture != null)
                {
                    _callback?.Invoke(m_DictionaryTextureModels[_id].texture);
                }
                else
                {
                    m_DictionaryTextureModels[_id].assetReference
                        .LoadAssetAsync<Texture2D>().Completed += (operation) =>
                    {
                        m_DictionaryTextureModels[_id].texture = operation.Result;
                        _callback?.Invoke(m_DictionaryTextureModels[_id].texture);
                    };
                }
            }
            else
            {
                Debug.LogError("TexturesLoader.LoadTexture() : textureModels does not contain key : " + _id);
            }
        }

        public void LoadTextureByAddress(string _address, Action<Texture2D> _Callback)
        {
            foreach (var keyValuePair in m_DictionaryTextureModels)
            {
                if (keyValuePair.Value.address.Equals(_address) == false)
                {
                    continue;
                }
                
                if (keyValuePair.Value.texture != null)
                {
                    _Callback?.Invoke(keyValuePair.Value.texture);
                }
                else
                {
                    keyValuePair.Value.assetReference.LoadAssetAsync<Texture2D>().Completed += (operation) =>
                    {
                        keyValuePair.Value.texture = operation.Result;
                        _Callback?.Invoke(keyValuePair.Value.texture);
                    };
                }
            }
        }

    }
}
