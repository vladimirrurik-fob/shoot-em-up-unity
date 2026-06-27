using NUnit.Framework;
using UnityEngine;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class BackgroundScrollerTests
    {
        [Test]
        public void Next_MovesDownward_AtScrollSpeed()
        {
            var scroller = new BackgroundScroller(10f, 0f, 1f, 0f, 0f);

            Vector3 next = scroller.Next(new Vector3(0f, 5f, 0f), 1f);

            Assert.AreEqual(new Vector3(0f, 4f, 0f), next);
        }

        [Test]
        public void Next_ResetsToStart_WhenPastEnd()
        {
            var scroller = new BackgroundScroller(10f, 0f, 1f, 0f, 0f);

            Vector3 next = scroller.Next(new Vector3(0f, -1f, 0f), 1f);

            Assert.AreEqual(new Vector3(0f, 10f, 0f), next);
        }
    }
}
