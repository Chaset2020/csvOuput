using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace csvOuput
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TextFieldParser parser = new TextFieldParser(@"C:\whova.csv"))
            {
                //Specify that the values will be separated by commas.
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    //Converts the Live Stream Watch Duration (in minutes) and Recorded Video Watch Duration (in minutes) fields to integers.
                    int streamWatchDuration;
                    Int32.TryParse(fields[4], out streamWatchDuration);

                    int videoWatchDuration;
                    Int32.TryParse(fields[5], out videoWatchDuration);
                    
                    //Sets the location of the resulting file.
                    string outputFile = @"C:\output.csv";

                    //Tests whether the sum of Live Stream Watch Duration and Recorded Video Watch Duration is greater than 40 and whether an email address is present.
                    //Creates output.csv if not present. Creates the header row for the new file.
                    if (streamWatchDuration + videoWatchDuration > 40 && fields[1] != "" && !File.Exists(outputFile))
                    {
                      StringBuilder stringBuilder = new StringBuilder();
                      stringBuilder.Append("Name,Email,Title,Total Minutes\n");
                      stringBuilder.AppendLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + (streamWatchDuration + videoWatchDuration));
                      File.WriteAllText(outputFile, stringBuilder.ToString());
                    }
                    else if (streamWatchDuration + videoWatchDuration > 40 && fields[1] != "") {
                      StringBuilder stringBuilder = new StringBuilder();
                      stringBuilder.AppendLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + (streamWatchDuration + videoWatchDuration));
                      File.AppendAllText(outputFile, stringBuilder.ToString());
                    }
                }
            }
        }
    }
}
