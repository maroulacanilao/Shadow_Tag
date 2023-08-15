using Controller;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class ShadowSpawnIndicator : MonoBehaviour
    {
        [SerializeField] private Transform sprite;
        [SerializeField] private Image bar;

        public void SetProgress(float progress_)
        {
            bar.fillAmount =  1f - progress_;
        }

        public void SetPosition(MovementInfo info_)
        {
            transform.position = info_.position;
            sprite.rotation = info_.rotation;
        }
        
        public void SetPosition(Vector3 position_)
        {
            transform.position = position_;
        }
    }
}
