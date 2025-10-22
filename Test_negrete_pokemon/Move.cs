namespace Test_negrete_pokemon
{
    public class Move
    {
        // campos simples (estilo básico)
        public string Name;
        public int BasePower; // 1-255, por defecto 100
        public int Speed;     // 1-5, por defecto 1 (no usado en fórmula)
        public ElementType Type;
        public MoveCategory Category;

        public Move()
        {
            Name = "Move";
            BasePower = 100;
            Speed = 1;
            Type = ElementType.Normal;
            Category = MoveCategory.Physical;
        }

        public Move(string name, ElementType type, MoveCategory category, int basePower = 100, int speed = 1)
        {
            Name = name;
            Type = type;
            Category = category;
            BasePower = basePower;
            Speed = speed;
        }
    }
}
