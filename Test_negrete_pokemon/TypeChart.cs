namespace Test_negrete_pokemon
{
    public static class TypeChart
    {
        // calcula MOD multiplicando la efectividad sobre cada tipo del defensor
        public static double CalculateModifier(ElementType attackType, ElementType[] defenderTypes)
        {
            if (defenderTypes == null || defenderTypes.Length == 0) return 1.0;
            double mod = 1.0;
            foreach (var dt in defenderTypes)
            {
                mod *= GetEffectiveness(attackType, dt);
            }
            return mod;
        }

        // función básica con ifs/switch: retorna 2.0, 0.5, 0.0 o 1.0
        private static double GetEffectiveness(ElementType attackType, ElementType defType)
        {
            // Agua
            if (attackType == ElementType.Water)
            {
                if (defType == ElementType.Fire) return 2.0;
                if (defType == ElementType.Ground) return 2.0;
                if (defType == ElementType.Rock) return 2.0;
                if (defType == ElementType.Grass) return 0.5;
            }

            // Fuego
            if (attackType == ElementType.Fire)
            {
                if (defType == ElementType.Grass) return 2.0;
                if (defType == ElementType.Water) return 0.5;
                if (defType == ElementType.Fire) return 0.5;
                if (defType == ElementType.Rock) return 0.5;
            }

            // Planta
            if (attackType == ElementType.Grass)
            {
                if (defType == ElementType.Water) return 2.0;
                if (defType == ElementType.Fire) return 0.5;
                if (defType == ElementType.Rock) return 2.0;
                if (defType == ElementType.Ground) return 2.0;
            }

            // Eléctrico
            if (attackType == ElementType.Electric)
            {
                if (defType == ElementType.Water) return 2.0;
                if (defType == ElementType.Grass) return 0.5;
                if (defType == ElementType.Ground) return 0.0; // inmunidad
            }

            // Normal / otros no especificados: 1.0
            return 1.0;
        }
    }
}
