using System.Collections;
using System.Collections.Generic;
using UI;
using Units.Types;
using UnityEngine;

public class UIInfoDisplay : UIElement
{
   [SerializeField] private Writer coinWriter;
   [SerializeField] private Writer nexusHealthWriter;

   private PlayerUnit _player;
   private Nexus _nexus;

   public override void Init()
   {
      base.Init();
      _player = FindObjectOfType<PlayerUnit>();
      _nexus = FindObjectOfType<Nexus>();
      coinWriter.Init();
      nexusHealthWriter.Init();
   }

   public override void Refresh()
   {
      coinWriter.SetText(_player.Gold.ToString());
      nexusHealthWriter.SetText(_nexus.currentHp + "/" + _nexus.FullHp);
   }
}
