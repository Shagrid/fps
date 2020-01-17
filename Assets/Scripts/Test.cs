using UnityEngine;

namespace Geekbrains
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<FlashLightModel>().Layer = 2;
        }
    }
}
