using System.Collections;
using UnityEngine;

namespace CustomHelpers
{
    public static class GeneralHelper
    {
        public static bool IsApproximatelyTo(this float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        /// <summary>
        /// probability range should be between 0 and 1
        /// </summary>
        /// <param name="probability_"></param>
        /// <returns></returns>
        public static bool RandomBool(float probability_)
        {
            return Random.Range(0, 1f) < probability_;
        }

        /// <summary>
        /// 50% chance of returning true
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            return Random.Range(0, 1f) < 0.5f;
        }

        public static IEnumerator WaitForAnimationEnd(this Animator animator_, int layerIndex_ = 0, int index_ = 0)
        {
            // var clipLength = animator_.GetCurrentAnimatorClipInfo(layerIndex_)[index_].clip.length;
            // var clipSpeed = animator_.GetCurrentAnimatorStateInfo(layerIndex_).speed;

            yield return new WaitWhile(() => animator_.GetCurrentAnimatorStateInfo(0).normalizedTime <= .99f);
        }

        public static int GetRandomInRange(this Vector2Int source)
        {
            return Random.Range(source.x, source.y + 1);
        }

        public static int EvaluateScaledCurve(this AnimationCurve curve_, int xToEvaluate_, int maxX_, int maxY_)
        {
            var _scaledX = (float) xToEvaluate_ / maxX_;
            var _scaledY = curve_.Evaluate(_scaledX);
            return (int) (_scaledY * maxY_);
        }
        
        public static float EvaluateScaledCurve(this AnimationCurve curve_, float xToEvaluate_, float maxX_, float maxY_)
        {
            var _scaledX = xToEvaluate_ / maxX_;
            var _scaledY = curve_.Evaluate(_scaledX);
            return _scaledY * maxY_;
        }
    }
}