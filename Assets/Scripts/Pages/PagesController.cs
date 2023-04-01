using System;
using System.Linq;

using UnityEngine;

using ANappTestTask.Pages.PageScripts;

using DG.Tweening;


namespace ANappTestTask.Pages
{
    public enum PageType { Menu, Levels, Options }

    public class PagesController : MonoBehaviour
    {
        public static Action<PageType> OnOpenPage;
        public static Action<PageType> OnOpenPageImmediately;

        public static Action<bool> OnSetLock;

        private PageScript _currentPage;

        [SerializeField] private PageSettings[] _pages; 
        [SerializeField] private GameObject _lock;

        public void Initialize()
        {
            OnOpenPage += OpenPage;
            OnOpenPageImmediately += OpenPageImmediately;

            OnSetLock += SetLock;
        }

        private void OpenPage(PageType pageType)
        {
            PageSettings pageSettings = _pages.First(x => x.Type == pageType);

            if (pageSettings == null)
                return;

            SetLock(true);

            _currentPage?.SetCanvasOrder(0);

            pageSettings.Page.OpenPage();
            pageSettings.Page.SetCanvasOrder(1);

            pageSettings.Page.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                _currentPage?.ClosePage();
                _currentPage = pageSettings.Page;

                SetLock(false);
            });
        }

        private void OpenPageImmediately(PageType pageType)
        {
            PageSettings pageSettings = _pages.First(x => x.Type == pageType);

            if (pageSettings == null)
                return;

            _currentPage?.ClosePage();
            _currentPage?.SetCanvasOrder(0);

            pageSettings.Page.OpenPageImmediately();
            pageSettings.Page.SetCanvasOrder(1);
            _currentPage = pageSettings.Page;
        }

        private void SetLock(bool isActive)
        {
            _lock.SetActive(isActive);
        }

        private void OnDestroy()
        {
            OnOpenPage = null;
            OnOpenPageImmediately = null;

            OnSetLock = null;
        }

        [Serializable]
        private class PageSettings
        {
            public PageScript _page;
            public PageScript Page { get => _page; }

            public PageType _type;
            public PageType Type { get => _type; }
        }
    }
}