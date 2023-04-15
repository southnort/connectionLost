using ConnectionLost.Core;


namespace ConnectionLost.Models.Enemy
{
    public sealed class CoreModel : EnemyBase
    {
        public override bool IsBlock => false;
        public override bool IsCanBlocked => true;
        public GridDifficult CoreDifficult { get; internal set; }
    }
}
