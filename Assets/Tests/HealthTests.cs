using NUnit.Framework;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class HealthTests
    {
        [Test]
        public void TakeDamage_ReducesHitPoints_ButStaysAlive()
        {
            var health = new Health(5);

            health.TakeDamage(2);

            Assert.IsTrue(health.IsAlive);
        }

        [Test]
        public void TakeDamage_ToZero_RaisesDied_AndIsNotAlive()
        {
            var health = new Health(1);
            bool died = false;
            health.Died += () => died = true;

            health.TakeDamage(1);

            Assert.IsFalse(health.IsAlive);
            Assert.IsTrue(died);
        }

        [Test]
        public void TakeDamage_AfterDeath_DoesNotRaiseDiedTwice()
        {
            var health = new Health(1);
            int count = 0;
            health.Died += () => count++;

            health.TakeDamage(1);
            health.TakeDamage(1);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Reset_RestoresAlive()
        {
            var health = new Health(1);
            health.TakeDamage(1);

            health.Reset(3);

            Assert.IsTrue(health.IsAlive);
        }
    }
}
