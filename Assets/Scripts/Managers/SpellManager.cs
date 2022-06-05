using BatteObjects;
using General;

namespace Managers
{
    public enum SpellType {Explositon, Lightning,Fire,Blood}

    public class SpellManager : Manager<Spell, SpellType, Spell.Args, SpellManager>
    {
        protected override string PrefabLocation => "Prefabs/Spells/";
    }
}