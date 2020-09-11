namespace BowlingKata
{
    /// <summary>
    /// Represents a frame in bowling. This frame can have up to 3 rolls.
    /// </summary>
    class Frame
    {
        static int MAX_ROLLS = 3;

        public char[] rolls = new char[MAX_ROLLS];

        public int totalScore
        { get; set; }

        public bool isFinalFrame
        { get; set; }

        public bool isStrike
        { get; set; }

        public bool isSpare
        { get; set; }
    }
}
