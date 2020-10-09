using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class StringExtentions
{
    public static string Pascal(string s)
    {
        System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder();

        foreach (char c in s)
        {
            // Replace anything, but letters and digits, with space
            if (!Char.IsLetterOrDigit(c))
            {
                resultBuilder.Append(" ");
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        string result = resultBuilder.ToString();

        // Make result string all lowercase, because ToTitleCase does not change all uppercase correctly
        result = result.ToLower();

        // Creates a TextInfo based on the "en-US" culture.
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        result = myTI.ToTitleCase(result);//.Replace(" ", String.Empty);

        return result;
    }
}
