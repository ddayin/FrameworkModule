using System;
using System.Collections.Generic;
using Skywatch.AssetManagement;
using Skywatch.AssetManagement.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework
{
    /// <summary>
     /// Sprite Atlas를 메모리 상에 적재하고 반환한다.
     /// </summary>
    [CreateAssetMenu(menuName = "Framework/SpriteAtlasContainer")]
    public class SpriteAtlasContainer : ScriptableObject
    {
        [SerializeField] private List<AssetReference> m_ListSpriteAtlasReference;
        
        public bool LoadSprite(string _id)
        {
            // Sprite
            
            // AssetManager.TryGetOrLoadObjectAsync()
            // m_ListSpriteAtlasReference[0].InstantiateAsync(Vector)
            
            
            return true;
        }
        
        public Sprite GetSprite(string _name)
        {

            return null;
        }
    }
}
