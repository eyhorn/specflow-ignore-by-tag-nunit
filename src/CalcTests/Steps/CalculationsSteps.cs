using System;
using Calc;
using CalcTests.Constants;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CalcTests.Steps
{
    [Binding]
    public class CalculationsSteps
    {
        private readonly ScenarioContext _context;

        public CalculationsSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I have provided (.*) and a (.*)")]
        public void GivenIHaveProvidedAndA(int firstNumber, int secondNumber)
        {
            _context.Set(firstNumber, ContextConstants.FirstNumber);
            _context.Set(secondNumber, ContextConstants.SecondNumber);
        }

        [When(@"I perform operation '(.*)'")]
        public void WhenIPerformOperation(string operationName)
        {
            var firstNumber = _context.Get<int>(ContextConstants.FirstNumber);
            var secondNumber = _context.Get<int>(ContextConstants.SecondNumber);
            var result = PerformOperation(operationName, firstNumber, secondNumber);
            _context.Set(result, ContextConstants.ResultNumber);
        }

        private static int PerformOperation(string operationName, int firstNumber, int secondNumber)
        {
            switch (operationName)
            {
                case nameof(Calculations.Add):
                    return firstNumber + secondNumber;
                case nameof(Calculations.Sub):
                    return firstNumber - secondNumber;
                case nameof(Calculations.Multiply):
                    return firstNumber * secondNumber;
                case nameof(Calculations.Divide):
                    return firstNumber / secondNumber;
            }
            throw new ArgumentException($"Unsupported operation '{operationName}'");
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedResultNumber)
        {
            var actualResultNumber = _context.Get<int>(ContextConstants.ResultNumber);

            Assert.AreEqual(expectedResultNumber, actualResultNumber);
        }
    }
}