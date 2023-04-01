using UnityEngine;


namespace ANappTestTask.Pages.PageScripts
{
    public abstract class PageScript : MonoBehaviour
    {
        private Canvas _canvas;

        protected bool _isInitialized;

        public virtual void OpenPage()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;

            Initialize();
        }

        public virtual void OpenPageImmediately()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;

            Initialize();
        }

        public void ClosePage()
        {
            gameObject.SetActive(false);
        }

        public void SetCanvasOrder(int order)
        {
            _canvas.sortingOrder = order;
        }

        public virtual void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _canvas = GetComponentInChildren<Canvas>();
        }
    }
}