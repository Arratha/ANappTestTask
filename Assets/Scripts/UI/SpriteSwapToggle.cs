using UnityEngine;
using UnityEngine.UI;


namespace ANappTestTask.UI
{
    [RequireComponent(typeof(Image))]
    public class SpriteSwapToggle : MonoBehaviour
    {
        private Image _image;

        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetToggleActive(bool isActive)
        {
            _image.sprite = (isActive) ? _spriteOn : _spriteOff;
        }
    }
}