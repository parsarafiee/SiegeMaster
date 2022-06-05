using System.Linq;
using General;
using UI;
using UnityEngine;

namespace Managers
{
   public class UIManager : Manager<UIElement, UIManager>
   {
      public override void Init()
      {
         foreach (var uiElement in Object.FindObjectsOfType<UIElement>().ToList())
         {
            Add(uiElement);
         }

         base.Init();
      }
   }
      
}