using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class CoreModel : EnemyBase
    {
        public CoreModel(float hp, float dmg, GridDifficult difficult) : base(hp, dmg) { CoreDifficult = difficult; }

        public override bool IsBlock => false;
        public override bool IsCanBlocked => true;
        public GridDifficult CoreDifficult { get; private set; }
    }
}
