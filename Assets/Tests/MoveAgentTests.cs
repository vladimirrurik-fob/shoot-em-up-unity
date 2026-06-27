using NUnit.Framework;
using UnityEngine;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class MoveAgentTests
    {
        [Test]
        public void SetDestination_MarksNotReached()
        {
            var agent = new MoveAgent();

            agent.SetDestination(Vector2.zero);

            Assert.IsFalse(agent.IsReached);
        }

        [Test]
        public void NextDirection_ReturnsNormalizedVector_TowardDestination()
        {
            var agent = new MoveAgent(0.25f);
            agent.SetDestination(new Vector2(10f, 0f));

            Vector2 direction = agent.NextDirection(Vector2.zero);

            Assert.AreEqual(new Vector2(1f, 0f), direction);
            Assert.IsFalse(agent.IsReached);
        }

        [Test]
        public void NextDirection_ReachesDestination_WhenWithinThreshold()
        {
            var agent = new MoveAgent(1f);
            agent.SetDestination(new Vector2(0.5f, 0f));

            Vector2 direction = agent.NextDirection(Vector2.zero);

            Assert.IsTrue(agent.IsReached);
            Assert.AreEqual(Vector2.zero, direction);
        }
    }
}
