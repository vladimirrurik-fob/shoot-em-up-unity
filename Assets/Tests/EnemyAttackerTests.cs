using NUnit.Framework;
using UnityEngine;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class EnemyAttackerTests
    {
        [Test]
        public void Tick_DoesNotFire_WhenCannotFire()
        {
            var attacker = new EnemyAttacker(1f);
            bool fired = false;
            attacker.FireRequested += (p, d) => fired = true;

            attacker.Tick(false, Vector2.zero, Vector2.up, 2f);

            Assert.IsFalse(fired);
        }

        [Test]
        public void Tick_Fires_AfterCountdownElapses()
        {
            var attacker = new EnemyAttacker(1f);
            Vector2? firePosition = null;
            attacker.FireRequested += (position, direction) => firePosition = position;

            attacker.Tick(true, new Vector2(1f, 1f), new Vector2(1f, 5f), 1.5f);

            Assert.IsNotNull(firePosition);
        }

        [Test]
        public void Tick_DirectionAimedAtTarget()
        {
            var attacker = new EnemyAttacker(0.5f);
            Vector2 firedDirection = Vector2.zero;
            attacker.FireRequested += (position, direction) => firedDirection = direction;

            attacker.Tick(true, Vector2.zero, new Vector2(0f, 10f), 1f);

            Assert.AreEqual(new Vector2(0f, 1f), firedDirection);
        }
    }
}
