using UnityEngine;
using UnityEngine.UI;


namespace ANappTestTask.Audio.Buttons
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();

            SetSourceActive(AudioController.IsSoundsActive);
            AudioController.OnChangeSoundsState += SetSourceActive;

            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => _source.Play());
        }

        private void SetSourceActive(bool isActive)
        {
            _source.volume = (isActive) ? 100 : 0;
        }

        private void OnDestroy()
        {
            AudioController.OnChangeSoundsState -= SetSourceActive;
        }
    }
}