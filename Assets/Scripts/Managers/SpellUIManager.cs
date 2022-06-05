using General;

namespace Managers
{
    public enum SpellUIType
    {
        None,
        DefaultRoundGround,
        ArcherTower,
        FireBallTower,
        CatapultTower,
        SeekingTower,
        Explositon,
        Lightning,
        Fire,
        Blood,
    }
    public class SpellUIManager : Manager<SpellUI, SpellUIType, SpellUI.Args, SpellUIManager>
    {
        protected override string PrefabLocation { get; } = "Prefabs/SpellUI/";
    }
}