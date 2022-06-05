using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IconAndText : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text text;
        public void SetText(string toWrite)
        {
            text.text = toWrite;
        }

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}