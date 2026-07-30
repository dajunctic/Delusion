using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Dajunctic
{
    public class PlayerRigAnim: BaseMono
    {
        [SerializeField] Rig leftHandRig;
        [SerializeField] Rig rightHandRig;

        public void SetAnim(RigAnimation anim)
        {
            switch (anim)
            {
                case RigAnimation.Grab:
                    leftHandRig.weight = 1f;
                    rightHandRig.weight = 1f;
                    break;
                default:
                    leftHandRig.weight = 0f;
                    rightHandRig.weight = 0f;
                    break;
            }
        }

    }
    
    public enum RigAnimation
    {
        None,
        Grab
    }
}