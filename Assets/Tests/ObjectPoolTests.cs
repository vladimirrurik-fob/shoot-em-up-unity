using NUnit.Framework;

namespace ShootEmUp.Tests
{
    [TestFixture]
    public sealed class ObjectPoolTests
    {
        [Test]
        public void Get_CreatesViaFactory_WhenEmpty()
        {
            int created = 0;
            var pool = new ObjectPool<string>(() =>
            {
                created++;
                return "item" + created;
            });

            string item = pool.Get();

            Assert.AreEqual("item1", item);
            Assert.AreEqual(1, created);
        }

        [Test]
        public void Release_ThenGet_ReusesItem()
        {
            var pool = new ObjectPool<object>(() => new object());
            object first = pool.Get();

            pool.Release(first);

            object second = pool.Get();
            Assert.AreSame(first, second);
        }

        [Test]
        public void OnGet_And_OnRelease_AreInvoked()
        {
            int gets = 0;
            int releases = 0;
            var pool = new ObjectPool<string>(
                factory: () => "x",
                onGet: _ => gets++,
                onRelease: _ => releases++);

            string a = pool.Get();
            pool.Release(a);
            string b = pool.Get();

            Assert.AreEqual(2, gets);
            Assert.AreEqual(1, releases);
        }
    }
}
