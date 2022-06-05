using System;
using Units.Types;

namespace Units.Statistics
{
    [Serializable]
    public class Stats
    {
        public Health health;
        public Mana mana;

        public void Init(Unit owner)
        {
            health.Init(owner);
            mana.Init(owner);
        }

        public void Refresh()
        {
            health.Refresh();
            mana.Refresh();
        }

    }
}