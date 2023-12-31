using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomHelpers
{
    public static class CollectionHelper
    {
        #region General

        /// <summary>
        /// returns true if the item was unique to the list and was successfully added
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemToAdd"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool AddUnique<T>(this IList<T> source, T itemToAdd)
        {
            if (source.Contains(itemToAdd)) return false;
            source.Add(itemToAdd);
            return true;
        }
        
        /// <summary>
        /// returns true if it was successfully moved
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemToMove_"></param>
        /// <param name="index_"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool MoveItem<T>(this IList<T> source, T itemToMove_, int index_)
        {
            if (!source.Remove(itemToMove_)) return false;
            source.Insert(index_,itemToMove_);
            return true;
        }
        
        /// <summary>
        /// returns true if it was successfully moved to first
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemToMove_"></param>
        /// <typeparam name="T"></typeparam>
        public static bool MoveItemToFirst<T>(this IList<T> source, T itemToMove_)
        {
            if (!source.Remove(itemToMove_)) return false;
            source.Insert(0,itemToMove_);
            return true;
        }
        
        /// <summary>
        /// returns true if it was successfully moved to last
        /// </summary>
        /// <param name="source"></param>
        /// <param name="itemToMove_"></param>
        /// <typeparam name="T"></typeparam>
        public static bool MoveItemToLast<T>(this IList<T> source, T itemToMove_)
        {
            if (!source.Remove(itemToMove_)) return false;
            source.Add(itemToMove_);
            return true;
        }

        #endregion

        #region Random Generic

        /// <summary>
        /// Shuffle the list in place using the Fisher-Yates method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("Cannot select a random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RemoveRandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new IndexOutOfRangeException("Cannot remove a random item from an empty list");
            var index = UnityEngine.Random.Range(0, list.Count);
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        #endregion

        #region Destroy Collection

        public static void DestroyGameObjects(this GameObject[] arr)
        {
            for (var i = arr.Length - 1; i > -1; --i)
            {
                UnityEngine.Object.Destroy(arr[i]);
            }
        }

        public static void DestroyGameObjects(this List<GameObject> list)
        {
            for (var i = list.Count - 1; i > -1; --i)
            {
                UnityEngine.Object.Destroy(list[i]);
                list.RemoveAt(i);
            }
            list.Clear();
        }

        public static void DestroyGameObjects(this IList<GameObject> list_)
        {
            for (var i = list_.Count - 1; i > -1; --i)
            {
                UnityEngine.Object.Destroy(list_[i]);
                list_.RemoveAt(i);
            }
            list_.Clear();
        }

        public static void DestroyComponents<T>(this IList<T> list) where T : Component
        {
            for (var i = list.Count - 1; i > -1; --i)
            {
                UnityEngine.Object.Destroy(list[i].gameObject);
                list.RemoveAt(i);
            }
            list.Clear();
        }
        

        public static void DestroyComponents(this Component[] list)
        {
            for (var i = list.Length - 1; i > -1; --i)
            {
                UnityEngine.Object.Destroy(list[i].gameObject);
            }
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        #endregion

        #region Dictionary

        public static void AddRange<K, V>(this IDictionary<K, V> source, IDictionary<K, V> ToAdd)
        {
            foreach (var _item in ToAdd)
            {
                source.Add(_item.Key, _item.Value);
            }
        }

        #endregion
    }
}