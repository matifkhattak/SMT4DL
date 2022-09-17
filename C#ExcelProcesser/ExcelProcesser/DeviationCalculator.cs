using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelProcesser
{
    public partial class DeviationCalculator : Form
    {
        public DeviationCalculator()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string[] validationLossArray = txtValidationLosses.Text.Split(',');
            decimal firstRowValue = 0;
            decimal secondRowValue = 0;
            decimal minValue = 999999999; //set it to some large value, so that first time condition gets true
            decimal maxValue = 0;
            decimal diffBetweenCells = 0;
            List<int> listOfRowsNotToProcess = new List<int>() { };
            string comparedValuesForGettingMax = "";
            string comparedValuesForGettingMin = "";
            string sameConsectivePairsValue = "";
            int rowNoToBeSkipped = 99;
            listOfRowsNotToProcess.Add(99);
            for (int i = 1; rowNoToBeSkipped < 2999; i++) //because currently we have 3000 rows to be processed, and the last index is of 2999
            {
                rowNoToBeSkipped = rowNoToBeSkipped + 100;
                listOfRowsNotToProcess.Add(rowNoToBeSkipped);
            }
            for(int i= 0;i< 2999;i++) //last index is of 2999 and the last pair to be compared is 2998 with 2999
            {
                if (listOfRowsNotToProcess.Any(x => x == i))
                    continue;
                    
                firstRowValue = Convert.ToDecimal(validationLossArray[i]);
                secondRowValue = Convert.ToDecimal(validationLossArray[i+1]);

                // Avoid -ive value
                if (firstRowValue > secondRowValue)
                {
                    diffBetweenCells = firstRowValue - secondRowValue;
                }
                else if (firstRowValue < secondRowValue)
                {
                    diffBetweenCells = secondRowValue - firstRowValue;
                }
                else // when firstRowValue = secondRowValue
                {
                    diffBetweenCells = secondRowValue - firstRowValue;
                    sameConsectivePairsValue = sameConsectivePairsValue + " , " + firstRowValue.ToString();
                }
                
                // record the maximum difference/deviation between any two cells/validation losses
                if (diffBetweenCells > maxValue)
                {
                    maxValue = diffBetweenCells;
                    comparedValuesForGettingMax = firstRowValue + " , " + secondRowValue;
                }
                // record the min difference/deviation between any two cells/validation losses
                if (diffBetweenCells < minValue)
                {
                    minValue = diffBetweenCells;
                    comparedValuesForGettingMin = firstRowValue + " , " + secondRowValue;
                }

            }
            lblMaxDifference.Text = maxValue.ToString();
            lblMinDifference.Text = minValue.ToString();
            lblComparedValuesForGettingMax.Text = comparedValuesForGettingMax.ToString();
            lblComparedValuesForGettingMin.Text = comparedValuesForGettingMin.ToString();
            lblSameValueForConsectivePairs.Text = sameConsectivePairsValue;


        }


        private double variance(int[] nums)
        {
            if (nums.Length > 1)
            {

                // Get the average of the values
                double avg = getAverage(nums);

                // Now figure out how far each point is from the mean
                // So we subtract from the number the average
                // Then raise it to the power of 2
                double sumOfSquares = 0.0;

                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - avg), 2.0);
                }

                // Finally divide it by n - 1 (for standard deviation variance)
                // Or use length without subtracting one ( for population standard deviation variance)
                return sumOfSquares / (double)(nums.Length - 1);
            }
            else { return 0.0; }
        }

        // Square root the variance to get the standard deviation
        private double standardDeviation(double variance)
        {
            return Math.Sqrt(variance);
        }

        // Get the average of our values in the array
        private double getAverage(int[] nums)
        {
            int sum = 0;

            if (nums.Length > 1)
            {

                // Sum up the values
                foreach (int num in nums)
                {
                    sum += num;
                }

                // Divide by the number of values
                return sum / (double)nums.Length;
            }
            else { return (double)nums[0]; }
        }


    }
}
