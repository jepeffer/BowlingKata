namespace BowlingKata
{
    /// <summary>
    /// Calculates a bowling score based off a bowling sheet or frame data.
    /// No data validation is done and it is assumed the data sent in will be correct!
    /// I.E. 
    /// We will not check for valid rolls.
    /// We will not check for correct number of rolls and frames.
    /// We will not provide scores for intermediate frames.
    /// This class is completed with no imports, raw C# code.
    /// ArrayList may have been cleaner though.
    /// </summary>
    public class BowlingScoreCalculator
    {
        private int MAX_FRAMES = 10; // Maximum number of frames in a bowling game is 10

        /// <summary>
        /// Takes the frame data and parses it to create Frames.
        /// The last frame can have between 1 and 3 rolls so the method accounts for that. This could've been hard coded I.E.
        /// If(splitframes[k].length == 1).... etc but I wrote a loop instead.
        /// </summary>
        /// <param name="frames">The frame data to parse</param>
        /// <returns>The new frame array with the proper frames</returns>
        private Frame[] GetFrameData(string frames)
        {
            Frame[] tempFramesArray = new Frame[MAX_FRAMES];
            string[] splitFrames = frames.Split(' ');
            int frameCounter = 0;
            for (int i = 0; i < splitFrames.Length; i++)
            {
                Frame newFrame = new Frame();

                // Counter that keeps track of which roll we are on
                int rollCounter = 0;

                if (frameCounter == MAX_FRAMES - 1) // Last frame
                {
                    for (int k = i; k < splitFrames.Length; k++)
                    {
                        for (int y = 0; y < splitFrames[k].Length; y++) // Add all the potential rolls (1-3)
                        {
                            newFrame.rolls[rollCounter++] = splitFrames[k][y];
                        }
                    }
                    newFrame.isFinalFrame = true;
                    tempFramesArray[frameCounter] = newFrame;
                    break;
                }

                if (splitFrames[i].Length == 1)
                {
                    newFrame.rolls[0] = splitFrames[i][0]; // Strike
                    newFrame.isStrike = true;
                    frameCounter++;
                }
                else if (splitFrames[i].Length == 2)
                {
                    newFrame.rolls[0] = splitFrames[i][0]; // Spare or less than 10 pins knocked down

                    if (splitFrames[i][1].Equals('/'))
                    {
                        newFrame.isSpare = true;
                    }
                    newFrame.rolls[1] = splitFrames[i][1];

                    frameCounter++;
                }
                tempFramesArray[i] = newFrame;
            }
            return tempFramesArray;
        }

        /// <summary>
        /// Calculates each frames total score, not accounting for lookaheads with strikes and spares.
        /// I.E. X = 10, 55 = 10, 5- = 5... etc
        /// </summary>
        /// <param name="frame">The frame to calculate the score</param>
        /// <returns>The new frame with an updated totalScore</returns>
        private Frame CalculateTotalFrameScore(Frame frame)
        {
            for (int i = 0; i < frame.rolls.Length; i++)
            {
                if (frame.rolls[i].Equals('\0')) // Empty roll
                {
                    break;
                }
                if (frame.rolls[i].Equals('X')) // Strike, the last frame can have multiple
                {
                    frame.totalScore += 10;
                }
                else if (frame.rolls[i].Equals('/')) // A spare will have a score of 10
                {
                    if (!frame.isFinalFrame)
                    {
                        frame.totalScore = 10;
                    }
                    else // Unless it is the final frame, which case it could have multiple spares or strikes/rolls
                    {
                        frame.totalScore += 10 - int.Parse(frame.rolls[i - 1].ToString());
                    }
                }
                else if (frame.rolls[i].Equals('-')) // Missed roll
                {
                    if (!frame.isFinalFrame)
                    {
                        break;
                    }
                }
                else // Int of pins, I.E. 5
                {
                    frame.totalScore += int.Parse(frame.rolls[i].ToString());
                }
            }

            return frame;
        }
        /// <summary>
        /// Accounts for the case where the frame is not a strike or a spare and just needs the char parsed into an int.
        /// If the char is a '-' then it is a 0 effectively.
        /// </summary>
        /// <param name="frame">The frame to get the integer roll from</param>
        /// <param name="rollsCounter">Current location in the rolls </param>
        /// <returns>The new integer that represents the next roll</returns>
        private int NoStrikeOrSpareRollValue(Frame frame, int rollsCounter)
        {
            int score = 0;
            if (!frame.rolls[rollsCounter].Equals('-'))
            {
                score += int.Parse(frame.rolls[rollsCounter].ToString());
            }
            return score;
        }
        /// <summary>
        /// Calculates the total score for strikes and spares with a lookahead integer.
        /// Strikes look ahead two rolls while spares look ahead one roll.
        /// </summary>
        /// <param name="framesArray">The Frames array to access the lookahead rolls</param>
        /// <param name="additionalRolls">The number of lookahead rolls</param>
        /// <param name="currentFrame">Represents which location in the frames array are we in</param>
        /// <returns>The newly calculated lookahead score</returns>
        private int CalculateStrikeOrSpareAdditionalRollsScore(Frame[] framesArray, int additionalRolls, int currentFrame)
        {
            int rollsCounter = 0;
            int score = 0;
            for (int y = currentFrame + 1; ;)
            {
                if (additionalRolls == 0) // We have reached our limit, break out of the loop
                {
                    break;
                }
                if (framesArray[y].rolls[rollsCounter].Equals('\0')) // End of char array, skip to the next frame
                {
                    additionalRolls++;
                    y++;
                    rollsCounter = 0;
                    continue;
                }
                else if (framesArray[y].isStrike) // A strike will always be 10
                {
                    score += 10;
                    y++;
                    rollsCounter = 0;
                }
                else if (framesArray[y].isSpare) // A spare only looks ahead once, so this looks at the very next roll
                {
                    if (rollsCounter == 0 && additionalRolls != 2)
                    {
                        score += NoStrikeOrSpareRollValue(framesArray[y], rollsCounter++);
                    }
                    else if (rollsCounter == 1)
                    {
                        score += 10;
                    }
                    y++;
                }
                else if (framesArray[y].isFinalFrame) // If it is the final frame it can be a strike, spare, and strike in the same roll
                {
                    if (framesArray[y].rolls[rollsCounter].Equals('X') || framesArray[y].rolls[rollsCounter].Equals('/'))
                    {
                        score += 10;
                        rollsCounter++;
                    }
                    else // It could also just be two misses
                    {
                        score += NoStrikeOrSpareRollValue(framesArray[y], rollsCounter++);
                    }
                }
                else
                {
                    score += NoStrikeOrSpareRollValue(framesArray[y], rollsCounter++); // If none of these cases were met we have a simple integer roll
                }
                additionalRolls--;
            }
            return score;
        }

        /// <summary>
        /// Adds up the score to create the total final scores. Loops through all of the frames and calculates its total score
        /// </summary>
        /// <param name="framesArray">The array to calculate the score off of</param>
        /// <returns>The total score</returns>
        private int ParseFrameScores(Frame[] framesArray)
        {
            int score = 0;
            int strikeAdditionalRolls = 2;
            int spareAdditionalRolls = 1;
            for (int i = 0; i < framesArray.Length; i++)
            {
                Frame frame = framesArray[i];

                if (!frame.isFinalFrame) // The final frame is just its total score
                {
                    if (frame.isStrike)
                    {
                        score += CalculateStrikeOrSpareAdditionalRollsScore(framesArray, strikeAdditionalRolls, i);
                    }
                    else if (frame.isSpare)
                    {
                        score += CalculateStrikeOrSpareAdditionalRollsScore(framesArray, spareAdditionalRolls, i);
                    }
                }
                score += frame.totalScore;
            }
            return score;
        }

        /// <summary>
        /// "Getter" for the score. This is the API endpoint for this library.
        /// Takes frame data and:
        /// 1. Parses the frame data into Frames
        /// 2. Calculates the total score of each Frame.
        /// 3. Calculates the lookahead score of each Frame. (Helps to have the total scores already calculated)
        /// </summary>
        /// <param name="frames">The frame data to be parsed</param>
        /// <returns>The final score</returns>
        public int GetScore(string frames)
        {
            Frame[] framesArray = GetFrameData(frames);
            for (int i = 0; i < framesArray.Length; i++)
            {
                framesArray[i] = CalculateTotalFrameScore(framesArray[i]);
            }
            return ParseFrameScores(framesArray);
        }
    }
}
