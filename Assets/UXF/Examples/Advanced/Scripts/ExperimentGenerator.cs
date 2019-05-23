using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace UXF
{

    public class ExperimentGenerator : MonoBehaviour
    {
        public UXF.Session session;

        public void Generate(Session session)
        {
            double[] angles = new double[] { 0, 45, 90 };
            double[] targetSizes = new double[] { .004, .015, .020 };

            int numPracticeTrials = 3;
            // create two blocks
            Block practiceBlock = session.CreateBlock(numPracticeTrials);
            Block block = session.CreateBlock(angles.Length * targetSizes.Length);

            // add catch trials
            MakePracticeTrials(practiceBlock, angles);
            MakeTrials(block, angles, targetSizes);

        }

        /// <summary>
        /// Modify a block by adding several catch trials and then shuffling the trial list.
        /// </summary>
        /// <param name="block"></param>
        void MakeTrials(Block block, double[] angles, double[] targetSizes)
        {
            int counterBecauseCSharpNoob = 0;
            foreach (double i in angles)
            {
                foreach (double j in targetSizes)
                {
                    Trial trial = block.trials[counterBecauseCSharpNoob];
                    trial.settings.SetValue("angle", i);
                    trial.settings.SetValue("targetSizes", j);
                    counterBecauseCSharpNoob++;
                }
            }

            // shuffle the trial order, so the catch trials are in random positions
            block.trials.Shuffle();
        }

        void MakePracticeTrials(Block block, double[] angles)
        {
            int counterBecauseCSharpNoob = 0;
            foreach (double i in angles)
            {
                Trial trial = block.trials[counterBecauseCSharpNoob];
                trial.settings.SetValue("angle", i);
                trial.settings.SetValue("targetSizes", 0.015);
                counterBecauseCSharpNoob++;
            }

            // shuffle the trial order, so the catch trials are in random positions
            block.trials.Shuffle();
        }

    }
}