

namespace ConnectionLost.Core
{
    public static class GameConfig
    {
        public const float OuterRadius = 10f;
        public const float InnerRadius = OuterRadius * 0.866025404f;

        public const int FirewallSpawnWeight = 35;
        public const int AntivirusSpawnWeight = 35;
        public const int HealerSpawnWeight = 14;
        public const int SuppressorSpawnWeight = 12;

        public const int CellsCountForGridDifficult = 12;
        public const float EnemiesPercentByGrid = 0.15f;

        public const float SuppressorDebuffValuePerLevel = 15f;
        public const float PlayerMinStrenght = 10f;
    }
}