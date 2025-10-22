namespace Test_negrete_pokemon
{
    public static class DamageCalculator
    {
        // Fórmula:
        // baseValue = ((2*level)/5 + 2) * power * (attack / defense)
        // damage = (baseValue / 50 + 2) * mod
        public static double CalculateDamage(int level, int power, int attack, int defense, double mod)
        {
            // validaciones básicas (para no dividir por 0)
            if (level < 1) level = 1;
            if (power < 1) power = 1;
            if (attack < 1) attack = 1;
            if (defense < 1) defense = 1;
            if (mod < 0) mod = 0;

            double baseValue = ((2.0 * level) / 5.0 + 2.0) * power * (attack / (double)defense);
            double damage = (baseValue / 50.0 + 2.0) * mod;
            return damage;
        }

        // helper que arma el llamado usando objetos Pokemon y Move
        public static double CalculateDamage(Pokemon attacker, Move move, Pokemon defender)
        {
            if (attacker == null || move == null || defender == null) return 0.0;

            int level = attacker.Level;
            int power = move.BasePower;
            int attack = (move.Category == MoveCategory.Physical) ? attacker.ATK : attacker.SpATK;
            int defense = (move.Category == MoveCategory.Physical) ? defender.DEF : defender.SpDEF;
            double mod = TypeChart.CalculateModifier(move.Type, defender.Types);

            return CalculateDamage(level, power, attack, defense, mod);
        }
    }
}
