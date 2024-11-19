using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;

namespace Framework
{
    public static class CommonUtils
    {
        /// <summary>
        /// 컴포넌트를 반환하려고 하는데 null 일 경우, AddComponent()를 통해 컴포넌트를 추가하고 반환한다.
        /// </summary>
        /// <param name="go"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            T component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }
        
        /// <summary>
        /// 지정한 Transform의 자식을 모두 삭제한다.
        /// </summary>
        /// <param name="trans"></param>
        public static void DestroyChildren(this Transform trans)
        {
            foreach (Transform child in trans)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// 설정한 Transform으로부터 자식을 생성한다.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="prefab"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform AddChildFromPrefab(this Transform trans, Transform prefab, string name = null)
        {
            Transform childTrans = GameObject.Instantiate(prefab) as Transform;
            childTrans.SetParent(trans, false);
            if (name != null)
            {
                childTrans.gameObject.name = name;
            }
            return childTrans;
        }

        /// <summary>
        /// DEBUG로 Symbol이 설정되어 있을 때만 Debug.Log()를 호출한다.
        /// </summary>
        /// <param name="message"></param>
        public static void DebugLog(string message)
        {
#if DEBUG
            Debug.Log(message);
#endif
        }
        
        /// <summary>
        /// DEBUG로 Symbol이 설정되어 있을 때만 Debug.LogWarning()를 호출한다.
        /// </summary>
        /// <param name="message"></param>
        public static void DebugLogWarning(string message)
        {
#if DEBUG
            Debug.LogWarning(message);
#endif
        }
        
        /// <summary>
        /// DEBUG로 Symbol이 설정되어 있을 때만 Debug.LogError()를 호출한다.
        /// </summary>
        /// <param name="message"></param>
        public static void DebugLogError(string message)
        {
#if DEBUG
            Debug.LogError(message);
#endif
        }

        /// <summary>
        /// 지정한 SecureString을 일반 문자열로 변환한다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(value);
            try
            {
                valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        
        /// <summary>
        /// Instantiate()하면서 생성하면서 바로 초기값 세팅한다.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="transformParent"></param>
        /// <param name="worldPositionStay"></param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject prefab, Transform transformParent, bool worldPositionStay = false)
        {
            GameObject newObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, transformParent);
            newObject.transform.SetParent(transformParent, worldPositionStay);
            return newObject;
        }

        /// <summary>
        /// Set Layer Recursively:
        /// This extension method sets the layer of a GameObject and all its children. It’s handy when you want to change the layer of an entire hierarchy.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursively(this GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
        
        /// <summary>
        /// Get Components In Parents
        /// Returns an array of components of a given type in the parent hierarchy of a GameObject. Useful when you need to access components on parent objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetComponentsInParents<T>(this GameObject obj) where T : Component
        {
            List<T> components = new List<T>();
            Transform parent = obj.transform.parent;
            while (parent != null)
            {
                components.AddRange(parent.GetComponents<T>());
                parent = parent.parent;
            }
            return components.ToArray();
        }

        /// <summary>
        /// Resetting the Transform
        /// Resets the position, rotation, and scale of a Transform to zero. Useful for resetting objects to their default state.
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Rotating a 2D Vector
        /// Returns a new vector that is rotated by a given angle. Useful for 2D gameplay mechanics.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, float angleInDegrees)
        {
            float radians = angleInDegrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            return new Vector2(vector.x * cos - vector.y * sin, vector.x * sin + vector.y * cos);
        }
        
        /// <summary>
        /// Serialization
        /// Converts an object to a byte array or vice versa. Useful for saving/loading game data.
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static byte[] SerializeToBytes<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Filter Child Components By Tag
        /// Returns a list of child components that have a certain tag. Useful for managing specific types of objects within a hierarchy.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetChildComponentsWithTag<T>(this Transform parent, string tag) where T : Component
        {
            List<T> components = new List<T>();
            foreach (Transform child in parent)
            {
                if (child.CompareTag(tag))
                {
                    components.Add(child.GetComponent<T>());
                }
            }
            return components;
        }
    }
}
