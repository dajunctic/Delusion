using UnityEngine;

namespace Dajunctic
{
    [CreateAssetMenu(fileName = "ComputerConfig", menuName = "Delusion/Config/ComputerConfig")]
    public class ComputerConfig: BaseConfig
    {
        [SerializeField] string password;

        public string Password => password;
    }

    public enum ComputerState
    {
        None,
        Boot,
        Lock,
        Login,
        Desktop
    }
}