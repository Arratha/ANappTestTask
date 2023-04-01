using UnityEngine;

using ANappTestTask.Pages;
using ANappTestTask.Audio;

namespace ANappTestTask.Core
{
    [RequireComponent(typeof(AudioController))]
    [RequireComponent(typeof(PagesController))]
    public class Initializer : MonoBehaviour
    {
        private void Start()
        {
            AudioController audioController = GetComponent<AudioController>();
            audioController.Initialize();

            PagesController pageController = GetComponent<PagesController>();
            pageController.Initialize();
            PagesController.OnOpenPageImmediately(PageType.Menu);
        }
    }
}