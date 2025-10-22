using System.Collections.Generic;

namespace Test_negrete_pokemon
{
    public class Pokemon
    {
        public string Name;
        public int Level;   // 1-99, default 1
        public int ATK;     // 1-255, default 10
        public int DEF;     // 1-255, default 10
        public int SpATK;   // 1-255, default 10
        public int SpDEF;   // 1-255, default 10
        public ElementType[] Types; // 1 or 2 types
        public List<Move> Moves;     // min 1, max 4

        // constructor por defecto
        public Pokemon()
        {
            Name = "Pokemon";
            Level = 1;
            ATK = 10;
            DEF = 10;
            SpATK = 10;
            SpDEF = 10;
            Types = new ElementType[] { ElementType.Normal };
            Moves = new List<Move>();
        }

        // constructor con nombre y tipos
        public Pokemon(string name, params ElementType[] types) : this()
        {
            Name = name;
            if (types != null && types.Length > 0)
            {
                // limitar a máximo 2 tipos
                if (types.Length == 1) Types = new ElementType[] { types[0] };
                else Types = new ElementType[] { types[0], types[1] };
            }
        }

        // helper simple para agregar movimiento manteniendo 1-4
        public void AddMove(Move m)
        {
            if (m == null) return;
            if (Moves.Count >= 4) return;
            Moves.Add(m);
        }
    }

    // 5 especies separadas (constructor por defecto define name y tipos)
    public class Pikachu : Pokemon
    {
        public Pikachu() : base("Pikachu", ElementType.Electric)
        {
            // valores por defecto (puedes ajustar si quieres)
        }
    }

    public class Charmander : Pokemon
    {
        public Charmander() : base("Charmander", ElementType.Fire) { }
    }

    public class Squirtle : Pokemon
    {
        public Squirtle() : base("Squirtle", ElementType.Water) { }
    }

    public class Bulbasaur : Pokemon
    {
        public Bulbasaur() : base("Bulbasaur", ElementType.Grass, ElementType.Poison) { }
    }

    public class Geodude : Pokemon
    {
        public Geodude() : base("Geodude", ElementType.Rock, ElementType.Ground)
        {
            ATK = 12;
            DEF = 14;
        }
    }
}
