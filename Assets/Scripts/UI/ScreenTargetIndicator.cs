using CustomHelpers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScreenTargetIndicator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Image image;
        [SerializeField] private bool hideWhenTargetIsOnScreen = true;

        private RectTransform rectTransform;
        private Camera cam;

        private void Awake()
        {
            rectTransform = image.rectTransform;
            cam = gameObject.scene.GetFirstMainCameraInScene();
            // if (canvas == null) canvas = rectTransform.GetCanvasOfRectTransform().GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (target == null) return;

            if (IsTargetBehind()) SetPositionBehind();
            else SetPosition();
        
            if (IsTargetOnScreen())
            {
                image.transform.rotation = Quaternion.Euler(0f, 0f, 0);
            }
            else RotateImageBasedOnPosition();
        }

        private void LateUpdate()
        {
            if (target == null) return;

            if (IsTargetBehind()) SetPositionBehind();
            else SetPosition();
            if (IsTargetOnScreen())
            {
                image.transform.rotation = Quaternion.Euler(0f, 0f, 0);
                image.enabled = !hideWhenTargetIsOnScreen;
            }
            else
            {
                if(!image.enabled) image.enabled = true;
                RotateImageBasedOnPosition();
            }
        }

        private void SetPosition()
        {
            Vector3 _screenPos = cam.WorldToScreenPoint(target.position);
            rectTransform.position = _screenPos;
            ClampPositionToCanvas(rectTransform,canvas);
        }

        private void SetPositionBehind()
        {
            Vector3 _objectPosition = target.position;
            Vector3 _viewportPosition = cam.WorldToViewportPoint(_objectPosition);

            if (_viewportPosition.z < 0f)
            {
                _viewportPosition.x = 1f - _viewportPosition.x;
                _viewportPosition.y = 1f - _viewportPosition.y;
            }

            Vector3 _screenPosition = cam.ViewportToScreenPoint(_viewportPosition);
            rectTransform.position = _screenPosition.SetY(-999);
            ClampPositionToCanvas(rectTransform,canvas);
        }

        public bool IsTargetBehind()
        {
            var _camPos = cam.WorldToViewportPoint(cam.transform.position);
            var _lookAtPos = cam.WorldToViewportPoint(target.position);

            return _camPos.z > _lookAtPos.z;
        }

        void RotateImageBasedOnPosition()
        {
            var _direction = Vector2.zero - image.rectTransform.anchoredPosition;

            var _rotationAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            var _rotation = Quaternion.Euler(0f, 0f, _rotationAngle - 90f);

            image.transform.rotation = _rotation;
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



        public void SetFollowObject(Transform followObject_)
        {
            target = followObject_;
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
    }
}
