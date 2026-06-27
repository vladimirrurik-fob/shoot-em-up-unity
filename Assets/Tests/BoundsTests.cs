using NUnit.Framework;
using UnityEngine;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class BoundsTests
    {
        [Test]
        public void InBounds_True_WhenInside()
        {
            var bounds = new Bounds(new Vector2(-1f, -1f), new Vector2(1f, 1f));

            bool result = bounds.InBounds(new Vector3(0f, 0f, 0f));

            Assert.IsTrue(result);
        }

        [Test]
        public void InBounds_False_WhenOutside()
        {
            var bounds = new Bounds(new Vector2(-1f, -1f), new Vector2(1f, 1f));

            bool result = bounds.InBounds(new Vector3(5f, 5f, 0f));

            Assert.IsFalse(result);
        }
    }
}
