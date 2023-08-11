using System;
using CustomHelpers;
using UnityEngine;

namespace UI
{
    public class ScreenTargetIndicator2D : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private RectTransform anchor;
        [SerializeField] private RectTransform icon;
        [SerializeField] private bool hideWhenTargetIsOnScreen = true;

        private Camera cam;
        private Transform player;

        private void Awake()
        {
            cam = gameObject.scene.GetFirstMainCameraInScene();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (IsTargetOnScreen()) anchor.gameObject.SetActive(!hideWhenTargetIsOnScreen);
            else if(!anchor.gameObject.activeSelf) anchor.gameObject.SetActive(true);
            
            // SetArrowPosition();
            RotateImageBasedOnPosition();
            StabilizeIcon();
        }

        private bool IsTargetOnScreen()
        {
            // Check if the target is behind the camera
            Vector3 targetViewportPos = cam.WorldToViewportPoint(target.position);
            if (targetViewportPos.z < 0f)
                return false;

            // Check if the target is within the view frustum of the camera
            return targetViewportPos.x >= 0f && targetViewportPos.x <= 1f &&
                   targetViewportPos.y >= 0f && targetViewportPos.y <= 1f;
        }

        private Quaternion GetRotation()
        {
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            
            return Quaternion.AngleAxis(angle, Vector2.down);
        }
        
        void RotateImageBasedOnPosition()
        {
            var _direction = player.position - target.position; 

            var _rotationAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            var _rotation = Quaternion.Euler(0f, 0f, _rotationAngle - 90f);

            anchor.rotation = _rotation;
        }

        private void SetArrowPosition()
        {
            Vector3 _screenPos = cam.WorldToScreenPoint(target.position);
            anchor.position = _screenPos;
            ClampPositionToCanvas(anchor,canvas);
        }
        
        public void ClampPositionToCanvas(RectTransform targetRectTransform, RectTransform canvasRectTransform)
        {
            Vector2 canvasSize = canvasRectTransform.sizeDelta;
            Vector2 targetSize = targetRectTransform.sizeDelta;

            Vector2 minPosition = (targetSize * 0.5f) - (canvasSize * 0.5f);
            Vector2 maxPosition = (canvasSize * 0.5f) - (targetSize * 0.5f);

            Vector2 clampedPosition = targetRectTransform.anchoredPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minPosition.x, maxPosition.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minPosition.y, maxPosition.y);

            targetRectTransform.anchoredPosition = clampedPosition;
        }

        private void StabilizeIcon()
        {
            if(icon == null) return;
            icon.rotation = Quaternion.Euler(0f, 0f, 0 - anchor.rotation.z);
        }
    }
}
