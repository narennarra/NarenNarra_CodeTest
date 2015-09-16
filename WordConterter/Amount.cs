using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Converter.BL
{
    public class Amount
    {
        StringBuilder wordNumber;
        string[] tens = new string[] { "ZERO ", " ONE ", " TWO ", " THREE ", " FOUR ", " FIVE ", " SIX ", " SEVEN ", " EIGHT ", " NINE ", " TEN ", " ELEVEN ", " TWELVE ", " THIRTEEN ", " FOURTEEN ", " FIFTEEN ", " SIXTEEN ", " SEVENTEEN ", " EIGHTEEN ", " NINETEEN " };
        string[] ones = new string[] { "ZERO ", " TEN ", " TWENTY ", " THIRTY ", " FORTY ", " FIFTY ", " SIXTY ", " SEVENTY ", " EIGHTY ", " NINETY " };

        public string AmounttoWords(string amount)
        {
            try
            {
                bool flagAsNegative = false;
                wordNumber = new StringBuilder();

                if (amount.Substring(0, 1) == "-")
                {
                    amount = amount.Substring(1, amount.Length - 1);
                    flagAsNegative = true;
                }
                if (Validateamount(amount))
                {
                    AmounttoWords(decimal.Parse(amount));
                }

                if (flagAsNegative && double.Parse(amount) > 0)
                    wordNumber.Insert(0, "MINUS ");

            }
            catch (Exception ex)
            {
                wordNumber.Append(ex.Message);
            }
            //Replace any extra spaces 
            Regex regex = new Regex(@"[ ]{2,}", RegexOptions.None);
            return regex.Replace(wordNumber.ToString().Trim(), @" ");
        }
        public void AmounttoWords(decimal amount)
        {
            //THis function os designed to work upto 13 digits i.e 9 followed by 12 digits 
            //Maximum Number is 9 999 999 999 999 
            //Rounds to 2 decimal places 

            if (amount == 0)
            {
                wordNumber.Append("ZERO DOLLAR");
            }
            else
            {
                amount = Math.Round(Math.Abs(amount), 2);
                string[] amountAsString = amount.ToString().Split('.');
                string dollarAmount = amountAsString[0];
                var amountMagnitude = (int)Math.Ceiling((double)(dollarAmount.Length) / 3);
                var amountString = dollarAmount;

                for (var i = amountMagnitude - 1; i >= 0; i--)
                {
                    //Split the number as 3 digit chunks focusing on from ones position 
                    var chunk = amountString.Substring(Math.Max(0, amountString.Length - (3 + (i * 3))), Math.Min(amountString.Length - (i * 3), 3));
                    if (Int32.Parse(chunk) == 0)
                        continue;

                    processthreedigitCurrency(chunk);


                    switch (i)
                    {
                        case 1:
                            wordNumber.Append(" THOUSAND ");
                            break;
                        case 2:
                            wordNumber.Append(" MILLION ");
                            break;
                        case 3:
                            wordNumber.Append(" BILLION ");
                            break;
                        case 4:
                            wordNumber.Append(" TRILLION ");
                            break;
                        default:
                            break;
                    }
                }

                if (double.Parse(dollarAmount) > 1)
                {
                    wordNumber.Append(" DOLLARS ");
                }
                else if (double.Parse(dollarAmount) > 0)
                {
                    wordNumber.Append(" DOLLAR ");
                }
                //Process Cents
                if (amountAsString.Length > 1)
                {
                    string centsAsString = amountAsString[1];
                    if (Int32.Parse(centsAsString) > 0)
                    {
                        processthreedigitCurrency(centsAsString);
                        if (Int32.Parse(centsAsString) > 9)
                        {
                            wordNumber.Append(" CENTS ");
                        }
                        else
                        {
                            wordNumber.Append(" CENT ");
                        }
                    }

                }
            }

        }

        private void processthreedigitCurrency(string chunk)
        {
            int currentChunk = Int32.Parse(chunk);


            if ((chunk.Length == 3) && (chunk[0] != '0'))
            {
                var test = tens.GetValue(Int32.Parse(chunk[0].ToString()));
                wordNumber.Append(test + " HUNDRED ");
                currentChunk %= 100;
            }

            if (currentChunk > 0)
            {
                if (wordNumber.ToString() != String.Empty)
                    wordNumber.Append("AND ");


                if (currentChunk < 20)
                    wordNumber.Append(tens.GetValue(currentChunk));
                else
                {
                    wordNumber.Append(ones.GetValue(currentChunk / 10));
                    if ((currentChunk % 10) > 0)
                        wordNumber.Append(tens.GetValue(currentChunk % 10));
                }
            }


        }

        private bool Validateamount(string input)
        {
            //validate amount
            //13 digits are allowed 
            //13 decimal places are allowed
            try
            {
                if (!isValidDecimal(input))
                    throw new FormatException(string.Format("Input is not valid. Value: {0}", input));

                var inputArr = input.Split('.');

                if (inputArr[0].Length > 13 || inputArr.Length > 13)
                    throw new FormatException(string.Format("Input is not valid. Value: {0}", input));

                var regex = new Regex(@"\d*(\.\d{0,13})$");
                if (inputArr.Length == 2 && !regex.Match(input).Success)
                    throw new FormatException(string.Format("Input is not valid. Value: {0}", input));

                return true;

            }
            catch (Exception ex)
            {
                wordNumber.Append(ex.Message);
                return false;
            }
        }

        private bool isValidDecimal(string input)
        {
            decimal i = 0;
            return decimal.TryParse(input, out i); //i now = 108
        }


    }
}