using UnityEngine;

namespace Dajunctic
{
    public interface IPlayer: IActor
    {
        public int SpeedHash { get; }

        public Vector2 GetMoveInput();
        public bool GetSprintInput();
    }
}