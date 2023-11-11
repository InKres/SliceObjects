using UnityEngine;

namespace SliceObjects.Module
{
    public class ObjectSlice : MonoBehaviour, ISliceModule
    {
        [Header("Components")]
        public GameObject fullObject;

        [Space]
        public GameObject halfObject;

        public void SliceObject(bool State)
        {
            if (fullObject == null)
            {
                Debug.LogError("FullObject is NULL!");
                return;
            }

            if (halfObject == null)
            {
                Debug.LogError("HalfObject is NULL!");
                return;
            }

            fullObject.SetActive(!State);
            halfObject.SetActive(State);
        }
    }
}