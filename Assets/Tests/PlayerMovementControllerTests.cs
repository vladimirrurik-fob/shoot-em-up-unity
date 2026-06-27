using NUnit.Framework;
using UnityEngine;

namespace ShootEmUp.Tests
{
    internal sealed class FakePlayerInput : ShootEmUp.IPlayerInput
    {
        public Vector2 MoveDirection { get; set; }

        public bool ConsumeFireRequest()
        {
            return false;
        }
    }

    internal sealed class RecordingMover : ShootEmUp.IMover
    {
        public Vector2 LastDirection { get; private set; }

        public int MoveCount { get; private set; }

        public void Move(Vector2 direction)
        {
            this.LastDirection = direction;
            this.MoveCount++;
        }
    }

    [TestFixture]
    public sealed class PlayerMovementControllerTests
    {
        [Test]
        public void Tick_MovesTowardInputDirection_ScaledByDeltaTime()
        {
            var input = new FakePlayerInput { MoveDirection = Vector2.right };
            var mover = new RecordingMover();
            var controller = new ShootEmUp.PlayerMovementController(input, mover);

            controller.Tick(0.1f);

            Assert.AreEqual(1, mover.MoveCount);
            Assert.AreEqual(new Vector2(0.1f, 0f), mover.LastDirection);
        }

        [Test]
        public void Tick_DoesNotMove_WhenNoInput()
        {
            var input = new FakePlayerInput { MoveDirection = Vector2.zero };
            var mover = new RecordingMover();
            var controller = new PlayerMovementController(input, mover);

            controller.Tick(0.1f);

            Assert.AreEqual(0, mover.MoveCount);
        }
    }
}
