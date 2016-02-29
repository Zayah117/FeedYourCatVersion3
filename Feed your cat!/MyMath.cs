using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feed_your_cat_
{
    class MyMath
    {
        Random random = new Random();
        private int number1;
        private int number2;
        private int answer;

        public void generateProblem(Label label)
        {
            number1 = random.Next(13);
            number2 = random.Next(13);
            answer = number1 * number2;
            label.Text = number1 + " x " + number2;
        }
        public bool checkAnswer(int number)
        {
            if (number == answer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
