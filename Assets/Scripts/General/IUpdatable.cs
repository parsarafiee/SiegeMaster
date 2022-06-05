
  namespace General
  {
    public interface IBootable
    {
      void Init();
      void PostInit();
    }
    public interface IUpdatable : IBootable
    {
      void Refresh();
      void FixedRefresh();
      void LateRefresh();
    }
  }

