namespace Calc
{
    public class Calculations : ICalculations
    {
        public int Add(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public int Sub(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }

        public int Multiply(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public int Divide(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }
    }
}