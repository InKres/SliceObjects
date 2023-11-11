using UnityEngine;

namespace SliceObjects.Module
{
    public class HalfObjectSlice : MonoBehaviour, ISliceModule
    {
        [Header("Component")]
        public GameObject halfObject;

        public void SliceObject(bool State)
        {
            halfObject.SetActive(!State);
        }
    }
}