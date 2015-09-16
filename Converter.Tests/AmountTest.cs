using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converter.BL;

namespace Converter.Tests
{
    [TestClass]
    public class AmountTest
    {
        private Converter.BL.Amount _amount = new Amount();

        [TestMethod]
        public void validPositiveInputString()
        {
            Assert.AreEqual("ZERO DOLLAR", _amount.AmounttoWords("0"));
            Assert.AreEqual("ONE DOLLAR", _amount.AmounttoWords("1"));
            Assert.AreEqual("NINE DOLLARS", _amount.AmounttoWords("9"));
            Assert.AreEqual("NINE DOLLARS", _amount.AmounttoWords("9.00000"));
            Assert.AreEqual("ONE HUNDRED AND ELEVEN DOLLARS", _amount.AmounttoWords("111"));
            Assert.AreEqual("ONE CENT", _amount.AmounttoWords(".01"));
            Assert.AreEqual("SEVEN DOLLARS AND TEN CENTS", _amount.AmounttoWords("7.1011"));
            Assert.AreEqual("NINETY SEVEN CENTS", _amount.AmounttoWords(".97"));
            Assert.AreEqual("NINETY NINE CENTS", _amount.AmounttoWords(".99"));
            Assert.AreEqual("ONE HUNDRED AND THIRTY THOUSAND FOUR HUNDRED AND TWENTY THREE DOLLARS AND FIVE CENT", _amount.AmounttoWords("130423.05"));
            Assert.AreEqual("TWELVE MILLION EIGHT HUNDRED AND EIGHTY NINE THOUSAND TWO HUNDRED AND ELEVEN DOLLARS AND SEVEN CENT", _amount.AmounttoWords("12889211.07"));
            Assert.AreEqual("ONE HUNDRED MILLION AND ONE DOLLARS AND ONE CENT", _amount.AmounttoWords("100000001.01"));
            Assert.AreEqual("NINE TRILLION NINE HUNDRED AND NINETY NINE BILLION NINE HUNDRED AND NINETY NINE MILLION NINE HUNDRED AND NINETY NINE THOUSAND NINE HUNDRED AND NINETY NINE DOLLARS AND NINETY NINE CENTS", _amount.AmounttoWords("9999999999999.99"));
            Assert.AreEqual("ONE HUNDRED AND TWENTY THREE DOLLARS AND FORTY FIVE CENTS", _amount.AmounttoWords("123.45"));
        }

        [TestMethod]
        public void ValidNegativeInputString()
        {
            Assert.AreEqual("MINUS ONE HUNDRED AND THIRTY THOUSAND FOUR HUNDRED AND TWENTY THREE DOLLARS AND FIVE CENT", _amount.AmounttoWords("-130423.05"));
            Assert.AreEqual("MINUS ONE DOLLAR AND FIFTEEN CENTS", _amount.AmounttoWords("-1.151"));
            Assert.AreEqual("ZERO DOLLAR", _amount.AmounttoWords("-0.00"));
            Assert.AreEqual("MINUS NINE TRILLION NINE HUNDRED AND NINETY NINE BILLION NINE HUNDRED AND NINETY NINE MILLION NINE HUNDRED AND NINETY NINE THOUSAND NINE HUNDRED AND NINETY NINE DOLLARS AND NINETY NINE CENTS", _amount.AmounttoWords("-9999999999999.99"));
        }

        [TestMethod]
        public void InvalidInputTest()
        {
            string input = "ABC";
            Assert.AreEqual(string.Format("Input is not valid. Value: {0}", input), _amount.AmounttoWords(input));
            input = "11A";
            Assert.AreEqual(string.Format("Input is not valid. Value: {0}", input), _amount.AmounttoWords(input));
            input = "abcd.012454";
            Assert.AreEqual(string.Format("Input is not valid. Value: {0}", input), _amount.AmounttoWords(input));
            input = "11111111111111111111111111111111111111.99999999999999999999999999999";
            Assert.AreEqual(string.Format("Input is not valid. Value: {0}", input), _amount.AmounttoWords(input));
            input = "111.111.111";
            Assert.AreEqual(string.Format("Input is not valid. Value: {0}", input), _amount.AmounttoWords(input));
        }

    }
}
