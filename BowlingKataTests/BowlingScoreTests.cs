using BowlingKata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingKataTests
{
    [TestClass]
    public class BowlingScoreTests
    {
        [TestMethod]
        public void AllStrikes()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(300, bowling.GetScore("X X X X X X X X X X X X"));
        }

        [TestMethod]
        public void NinesAndMisses()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(90, bowling.GetScore("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-"));
        }

        [TestMethod]
        public void FivesAndSpares()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(150, bowling.GetScore("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5"));
        }

        [TestMethod]
        public void AllMisses()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(0, bowling.GetScore("-- -- -- -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void AllButOne()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(5, bowling.GetScore("-- -- -- -- -- -- -- -- -- -5"));
        }

        [TestMethod]
        public void AllButASpare()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(10, bowling.GetScore("-- -- -- -- -- -- -- -- -- 5/-"));
        }

        [TestMethod]
        public void AllButAStrike()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(10, bowling.GetScore("-- -- -- -- -- -- -- -- -- X--"));
        }

        [TestMethod]
        public void StrikeFollowedByTwoIntegerRolls()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(241, bowling.GetScore("X 53 50 X X X X X X X X X"));
        }

        [TestMethod]
        public void SpareFollowedByTwoIntegerRolls()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(238, bowling.GetScore("5/ 53 50 X X X X X X X X X"));
        }

        [TestMethod]
        public void OnlyTheFirstFrameStrike()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(10, bowling.GetScore("X -- -- -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void OnlyTheFirstFrameSpare()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(10, bowling.GetScore("5/ -- -- -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void FrameNineStrikeFollowedByTwoStrikes()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(50, bowling.GetScore("-- -- -- -- -- -- -- -- X X X-"));
        }

        [TestMethod]
        public void FrameNineStrikeFollowedByOneStrikeAndASpare()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(45, bowling.GetScore("-- -- -- -- -- -- -- -- X X 5/"));
        }

        [TestMethod]
        public void SpareInTheMiddle()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(10, bowling.GetScore("-- -- -- 3/ -- -- -- -- -- --"));
        }

        [TestMethod]
        public void StrikesFollowedByIntegerRolls()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(176, bowling.GetScore("9/ 0/ X X 62 7/ 8/ X 9- X X 8"));
        }

        [TestMethod]
        public void SpareThenStrike()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(30, bowling.GetScore("9/ X -- -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void StrikeThenSpare()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(20, bowling.GetScore("X 9/ -- -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void SpareFollowedByIntegerRoll()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(32, bowling.GetScore("9/ 62 62 -- -- -- -- -- -- --"));
        }

        [TestMethod]
        public void AllIntegerRolls()
        {
            BowlingScoreCalculator bowling = new BowlingScoreCalculator();
            Assert.AreEqual(68, bowling.GetScore("12 34 54 33 53 33 51 15 81 80"));
        }
    }
}
