using NUnit.Framework;

namespace Test_negrete_pokemon
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        // 1) Pokémon - valores por defecto
        [Test]
        public void Test01_Pokemon_DefaultValues()
        {
            var p = new Pokemon();
            Assert.That(p.Level, Is.EqualTo(1));
            Assert.That(p.ATK, Is.EqualTo(10));
            Assert.That(p.DEF, Is.EqualTo(10));
            Assert.That(p.SpATK, Is.EqualTo(10));
            Assert.That(p.SpDEF, Is.EqualTo(10));
            Assert.That(p.Types.Length, Is.GreaterThanOrEqualTo(1));
            TestContext.WriteLine("✅ Test01_Pokemon_DefaultValues pasó.");
        }

        // 2) Move - valores por defecto
        [Test]
        public void Test02_Move_DefaultValues()
        {
            var m = new Move();
            Assert.That(m.BasePower, Is.EqualTo(100));
            Assert.That(m.Speed, Is.EqualTo(1));
            Assert.That(m.Type, Is.EqualTo(ElementType.Normal));
            Assert.That(m.Category, Is.EqualTo(MoveCategory.Physical));
            TestContext.WriteLine("✅ Test02_Move_DefaultValues pasó.");
        }

        // 3) Species - Pikachu tipo Electric
        [Test]
        public void Test03_Species_Pikachu()
        {
            var pika = new Pikachu();
            Assert.That(pika.Name, Is.EqualTo("Pikachu"));
            Assert.That(pika.Types[0], Is.EqualTo(ElementType.Electric));
            TestContext.WriteLine("✅ Test03_Species_Pikachu pasó.");
        }

        // 4) Species - Charmander tipo Fire
        [Test]
        public void Test04_Species_Charmander()
        {
            var c = new Charmander();
            Assert.That(c.Name, Is.EqualTo("Charmander"));
            Assert.That(c.Types[0], Is.EqualTo(ElementType.Fire));
            TestContext.WriteLine("✅ Test04_Species_Charmander pasó.");
        }

        // 5) Species - Squirtle tipo Water
        [Test]
        public void Test05_Species_Squirtle()
        {
            var s = new Squirtle();
            Assert.That(s.Name, Is.EqualTo("Squirtle"));
            Assert.That(s.Types[0], Is.EqualTo(ElementType.Water));
            TestContext.WriteLine("✅ Test05_Species_Squirtle pasó.");
        }

        // 6) Species - Bulbasaur tipo Grass/Poison
        [Test]
        public void Test06_Species_Bulbasaur()
        {
            var b = new Bulbasaur();
            Assert.That(b.Name, Is.EqualTo("Bulbasaur"));
            Assert.That(b.Types.Length, Is.EqualTo(2));
            Assert.That(b.Types, Does.Contain(ElementType.Grass));
            Assert.That(b.Types, Does.Contain(ElementType.Poison));
            TestContext.WriteLine("✅ Test06_Species_Bulbasaur pasó.");
        }

        // 7) Species - Geodude stats
        [Test]
        public void Test07_Species_Geodude()
        {
            var g = new Geodude();
            Assert.That(g.Name, Is.EqualTo("Geodude"));
            Assert.That(g.Types.Length, Is.EqualTo(2));
            Assert.That(g.ATK, Is.EqualTo(12));
            Assert.That(g.DEF, Is.EqualTo(14));
            TestContext.WriteLine("✅ Test07_Species_Geodude pasó.");
        }

        // 8) Type modifier: Water vs Fire -> 2x
        [Test]
        public void Test08_TypeModifier_Water_vs_Fire()
        {
            double mod = TypeChart.CalculateModifier(ElementType.Water, new[] { ElementType.Fire });
            Assert.That(mod, Is.EqualTo(2.0));
            TestContext.WriteLine("✅ Test08_TypeModifier_Water_vs_Fire pasó.");
        }

        // 9) Type modifier: Water vs Fire/Ground -> 4x
        [Test]
        public void Test09_TypeModifier_Water_vs_FireGround()
        {
            double mod = TypeChart.CalculateModifier(ElementType.Water, new[] { ElementType.Fire, ElementType.Ground });
            Assert.That(mod, Is.EqualTo(4.0));
            TestContext.WriteLine("✅ Test09_TypeModifier_Water_vs_FireGround pasó.");
        }

        // 10) Type modifier: Electric vs Fire/Ground -> 0x
        [Test]
        public void Test10_TypeModifier_Electric_vs_FireGround()
        {
            double mod = TypeChart.CalculateModifier(ElementType.Electric, new[] { ElementType.Fire, ElementType.Ground });
            Assert.That(mod, Is.EqualTo(0.0));
            TestContext.WriteLine("✅ Test10_TypeModifier_Electric_vs_FireGround pasó.");
        }

        // 11) Type modifier - varios pares
        [TestCase(ElementType.Fire, ElementType.Grass, 2.0)]
        [TestCase(ElementType.Fire, ElementType.Water, 0.5)]
        [TestCase(ElementType.Grass, ElementType.Water, 2.0)]
        [TestCase(ElementType.Electric, ElementType.Water, 2.0)]
        [TestCase(ElementType.Water, ElementType.Grass, 0.5)]
        public void Test11_TypeModifier_Multiple(ElementType atk, ElementType def, double expected)
        {
            double mod = TypeChart.CalculateModifier(atk, new[] { def });
            Assert.That(mod, Is.EqualTo(expected));
            TestContext.WriteLine($"✅ Test11_TypeModifier_Multiple {atk} vs {def} => {mod} pasó.");
        }

        // 12) Damage formula example: LV=50, PWR=128, ATK=128, DEF=128, MOD=1 => ~58.32
        [Test]
        public void Test12_DamageFormula_ExampleCase()
        {
            double dmg = DamageCalculator.CalculateDamage(50, 128, 128, 128, 1.0);
            Assert.That(dmg, Is.EqualTo(58.32).Within(0.01));
            TestContext.WriteLine($"✅ Test12_DamageFormula_ExampleCase pasó. Daño={dmg:F4}");
        }

        // 13) DamageCalculator uses Physical stats for Physical moves
        [Test]
        public void Test13_DamageCalculator_Physical_UsesATK_DEF()
        {
            var attacker = new Pokemon("A", ElementType.Fire) { Level = 30, ATK = 80, SpATK = 30 };
            var defender = new Pokemon("B", ElementType.Rock) { DEF = 60, SpDEF = 50 };
            var move = new Move("Headbutt", ElementType.Normal, MoveCategory.Physical, basePower: 60);

            double expected = DamageCalculator.CalculateDamage(attacker.Level, move.BasePower, attacker.ATK, defender.DEF,
                TypeChart.CalculateModifier(move.Type, defender.Types));
            double actual = DamageCalculator.CalculateDamage(attacker, move, defender);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
            TestContext.WriteLine($"✅ Test13_DamageCalculator_Physical_UsesATK_DEF pasó. Daño={actual:F4}");
        }

        // 14) DamageCalculator uses Special stats for Special moves
        [Test]
        public void Test14_DamageCalculator_Special_UsesSpATK_SpDEF()
        {
            var attacker = new Pokemon("A", ElementType.Water) { Level = 40, SpATK = 100, ATK = 30 };
            var defender = new Pokemon("B", ElementType.Fire) { SpDEF = 80, DEF = 50 };
            var move = new Move("Surf", ElementType.Water, MoveCategory.Special, basePower: 90);

            double expected = DamageCalculator.CalculateDamage(attacker.Level, move.BasePower, attacker.SpATK, defender.SpDEF,
                TypeChart.CalculateModifier(move.Type, defender.Types));
            double actual = DamageCalculator.CalculateDamage(attacker, move, defender);

            Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
            TestContext.WriteLine($"✅ Test14_DamageCalculator_Special_UsesSpATK_SpDEF pasó. Daño={actual:F4}");
        }

        // 15) DamageCalculator - Physical con modificador distinto de 1
        [Test]
        public void Test15_DamageCalculator_Physical_WithModifier()
        {
            var attacker = new Pokemon("A", ElementType.Water) { Level = 20, ATK = 60 };
            var defender = new Pokemon("B", ElementType.Fire) { DEF = 40 };
            var move = new Move("SplashHit", ElementType.Water, MoveCategory.Physical, basePower: 50);

            double mod = TypeChart.CalculateModifier(move.Type, defender.Types); // esperado: 2.0
            Assert.That(mod, Is.EqualTo(2.0));

            double dmg = DamageCalculator.CalculateDamage(attacker, move, defender);
            Assert.That(dmg, Is.GreaterThan(0));
            TestContext.WriteLine($"✅ Test15_DamageCalculator_Physical_WithModifier pasó. MOD={mod}, Daño={dmg:F4}");
        }

        // 16) DamageCalculator - inmunidad (MOD = 0)
        [Test]
        public void Test16_DamageCalculator_Special_WithImmunity()
        {
            var attacker = new Pokemon("A", ElementType.Electric) { Level = 30, SpATK = 90 };
            var defender = new Pokemon("B", ElementType.Fire, ElementType.Ground) { SpDEF = 60 };
            var move = new Move("Thunder", ElementType.Electric, MoveCategory.Special, basePower: 110);

            double mod = TypeChart.CalculateModifier(move.Type, defender.Types);
            Assert.That(mod, Is.EqualTo(0.0));

            double dmg = DamageCalculator.CalculateDamage(attacker, move, defender);
            Assert.That(dmg, Is.EqualTo(0.0));
            TestContext.WriteLine("✅ Test16_DamageCalculator_Special_WithImmunity pasó. MOD=0, Daño=0");
        }

        // 17) Límite de movimientos
        [Test]
        public void Test17_Pokemon_MovesCount_Constraints()
        {
            var p = new Pokemon("TestMon", ElementType.Normal);
            for (int i = 0; i < 6; i++)
                p.AddMove(new Move($"M{i}", ElementType.Normal, MoveCategory.Physical));

            Assert.That(p.Moves.Count, Is.LessThanOrEqualTo(4));
            Assert.That(p.Moves.Count, Is.GreaterThanOrEqualTo(0));
            TestContext.WriteLine($"✅ Test17_Pokemon_MovesCount_Constraints pasó. Moves={p.Moves.Count}");
        }

        // 18) Niveles válidos
        [Test]
        public void Test18_Pokemon_Level_DefaultAndSet()
        {
            var p = new Pokemon();
            Assert.That(p.Level, Is.EqualTo(1));
            p.Level = 99;
            Assert.That(p.Level, Is.EqualTo(99));
            TestContext.WriteLine("✅ Test18_Pokemon_Level_DefaultAndSet pasó.");
        }

        // 19) TypeChart devuelve 1.0 para pares desconocidos
        [Test]
        public void Test19_TypeChart_DefaultsToOneForUnknown()
        {
            double mod = TypeChart.CalculateModifier(ElementType.Normal, new[] { ElementType.Rock });
            Assert.That(mod, Is.EqualTo(1.0));
            TestContext.WriteLine("✅ Test19_TypeChart_DefaultsToOneForUnknown pasó.");
        }

        // 20) Independencia de instancias
        [Test]
        public void Test20_Tests_Independence_Basic()
        {
            var a = new Pokemon("A", ElementType.Fire) { Level = 10, ATK = 50 };
            var b = new Pokemon("B", ElementType.Water) { Level = 10, ATK = 60 };

            a.ATK = 1;
            Assert.That(b.ATK, Is.EqualTo(60));
            TestContext.WriteLine("✅ Test20_Tests_Independence_Basic pasó.");
        }
    }
}
