using TMPro;
using UnityEngine;
namespace GUI
{
    public class StatSlotGui : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ChangeText(string text)
        {
            _text.text = text;
        }
    }
}
