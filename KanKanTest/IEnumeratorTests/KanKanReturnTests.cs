using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanCore.Karass.Dependencies;
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.Karass;
using KanKanTest.Mocks.KarassFrame;
using KanKanTest.Mocks.KarassMocks;
using NUnit.Framework;

namespace KanKanTest.IEnumeratorTests
{
    public class KanKanReturnTests
    {
        public class GivenNoFrames
        {
            [Test]
            public void GivenNoFramesReturnFalse()
            {
                Karass karass = new KarassDummy();
                KanKan kankan = new KanKan(karass,  new FrameFactoryDummy());
                Assert.False(kankan.MoveNext());
            }
        }

        public class GivenMultipleFrames
        {
            [Test]
            public void WithTwoFramesReturnCorrectValues()
            {
                IKarass karass = new KarassNumberOfFramesStub(2);
                KanKan kankan = new KanKan(karass,   new FrameFactoryDummy());
                Assert.True(kankan.MoveNext());
                Assert.False(kankan.MoveNext());
            }

            [Test]
            public void WithThreeFramesReturnCorrectValues()
            {
                IKarass karass = new KarassNumberOfFramesStub(2);
                KanKan kankan = new KanKan(karass,   new FrameFactoryDummy());
                Assert.True(kankan.MoveNext());
                Assert.False(kankan.MoveNext());
            }

            [Test]
            public void WithMultipleFramesReturnCorrectValues()
            {
                int number = 42;
                IKarass karass = new KarassNumberOfFramesStub(number);
                KanKan kankan = new KanKan(karass,   new FrameFactoryDummy());
                for (int i = 0; i < number - 1; i++)
                {
                    bool returnValue = kankan.MoveNext();
                    Assert.True(returnValue);
                }

                Assert.False(kankan.MoveNext());
            }
        }
    }
}