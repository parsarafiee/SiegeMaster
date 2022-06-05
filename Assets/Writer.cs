using UI;
using UnityEngine;

public class Writer : MonoBehaviour
{
    private IconAndText _iconAndText;
    [SerializeField] private Sprite sprite;
    
    public void Init()
    {
        _iconAndText = GetComponent<IconAndText>();
        _iconAndText.SetIcon(sprite);
    }

    public void SetText(string message)
    {
        _iconAndText.SetText(message);
    }
}
