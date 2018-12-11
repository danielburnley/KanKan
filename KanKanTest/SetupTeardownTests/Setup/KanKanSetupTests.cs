using System;
using System.Collections.Generic;
using KanKanCore;
using KanKanCore.Factories;
using KanKanCore.Karass;
using KanKanTest.Mocks.Dependencies;
using KanKanTest.Mocks.UAction;
using Xunit;
using Xunit.Abstractions;

namespace KanKanTest.SetupTeardownTests.Setup
{
    public class KanKanSetupTests
    {
        private static KarassFactory KarassFactory => new KarassFactory(new DependenciesDummy());

        [Fact]
        void SetupIsRunOnMoveNext()
        {
            int setupCounter = 0;
            Action setup = () => { setupCounter++; };
            Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), new List<List<Action>>(),
                new List<Func<string, bool>[]>());

            KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
            actionRunner.MoveNext();
            Assert.True(setupCounter > 0);
        }

        public class GivenMultipleFrames
        {
            bool FrameOne(string message) => true;
            bool FrameTwo(string message) => true;

            private List<Func<string, bool>[]> Frames => new List<Func<string, bool>[]>
            {
                new Func<string, bool>[]
                {
                    FrameOne,
                    FrameTwo
                }
            };

            [Fact]
            void SetupIsRunOnFirstMoveNextOnly()
            {
                int setupCounter = 0;
                Action setup = () => { setupCounter++; };
                Karass testKarass = KarassFactory.Get(CreateActionListWith(setup), new List<List<Action>>(), Frames);

                KanKan actionRunner = new KanKan(testKarass, new KarassMessageDummy());
                actionRunner.MoveNext();
                Assert.True(setupCounter == 1);
                actionRunner.MoveNext();
                Assert.True(setupCounter == 1);
            }
        }

        private static List<List<Action>> CreateActionListWith(Action a) =>
            new List<List<Action>> {new List<Action> {a}};
    }
}