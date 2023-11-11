using SliceObjects.Module;
using SliceObjects.View;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SliceObjects
{
    public class SlicePresenter : MonoBehaviour
    {
        public event Action<bool> OnSliceStateChanged;

        [Header("View component")]
        [SerializeField] private SliceView sliceView;

        [Header("Presenter components")]
        [Tooltip("Object where all slice modules are located")]
        [SerializeField] private GameObject rootObjectOfModules;

        [Header("Settings")]
        [SerializeField] private bool isSliceState;
        [Space]
        [Tooltip("This flag allows you to change the start state of the slice")]
        [SerializeField] private bool isStartSliceState;

        private List<ISliceModule> sliceModules = new List<ISliceModule>();

        private void Start()
        {
            sliceView.OnButtonClick += ChangeSliceState;

            FindSliceModules();
        }

        /// <summary>
        /// Install new view component
        /// </summary>
        /// <param name="sliceView"></param>
        public void SetSliceView(SliceView sliceView)
        {
            this.sliceView = sliceView;
        }

        /// <summary>
        /// Install new slice modules
        /// </summary>
        /// <param name="sliceModules"></param>
        public void SetSliceModules(ISliceModule[] sliceModules)
        {
            sliceModules.AddRange(sliceModules.ToList());

            DebuggingOfSliceModules();
        }

        private void FindSliceModules()
        {
            if (rootObjectOfModules == null)
            {
                rootObjectOfModules = gameObject;
            }
            else
            {
                sliceModules.AddRange(rootObjectOfModules.GetComponentsInChildren<ISliceModule>(true));
            }

            SetStartSliceState();
        }

        private void SetStartSliceState()
        {
            if (isStartSliceState)
            {
                if (sliceModules.Count > 0)
                {
                    SliceObjects(true);
                }
            }
        }

        /// <summary>
        /// Change the state of the slice
        /// </summary>
        public void ChangeSliceState()
        {
            isSliceState = !isSliceState;

            SliceObjects(isSliceState);

            OnSliceStateChanged?.Invoke(isSliceState);
        }

        private void SliceObjects(bool State)
        {
            foreach (ISliceModule module in sliceModules)
            {
                module.SliceObject(State);
            }
        }

        /// <summary>
        /// Check which slice modules the presenter knows about
        /// </summary>
        public void DebuggingOfSliceModules()
        {
            for (int i = 0; i < sliceModules.Count; i++)
            {
                Debug.Log($"{i + 1}. Slice module \"{sliceModules[i].GetType().Name}\"");
            }
        }

        private void OnDestroy()
        {
            sliceView.OnButtonClick -= ChangeSliceState;
        }
    }
}