using UnityEngine;
using UnityEngine.UI;

public class FillableBar : MonoBehaviour
{
   [SerializeField] private Slider _slider;
   [SerializeField] private Text _text;

   public void Refresh(float slidePercentage, string text)
   {
      _slider.value = slidePercentage;
      _text.text = text;
   }
}
