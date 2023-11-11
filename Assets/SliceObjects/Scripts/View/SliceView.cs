using System;
using UnityEngine;
using UnityEngine.UI;

namespace SliceObjects.View
{
    public class SliceView : MonoBehaviour
    {
        public event Action OnButtonClick;

        [Header("View component")]
        [SerializeField] private Button sliceButton;

        private void Start()
        {
            sliceButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            OnButtonClick?.Invoke();
        }

        private void OnDestroy()
        {
            sliceButton.onClick.RemoveListener(OnClick);
        }
    }
}
