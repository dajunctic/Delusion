using UnityEngine;

namespace Dajunctic
{
    public interface IGrabInteractable: IInteractable
    {
        public GrabState State {get;}
        public void Pick(Transform container);
        public void Drop();
    }

    public enum GrabState
    {
        None,
        Picked
    }
}