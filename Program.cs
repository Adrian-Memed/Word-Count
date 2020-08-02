using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WordCounter
{
    class Program
    {
        // Summary:
        //    Get count of the 10 most common words from text file.
        //
        // Assumptions: 
        //     Text encoding = UTF8,
        //     .net core char IsPunctuation and IsSeparator used for punctuation and spacing definition.
        //
        // Parameters: 
        //     path of file containing the text.
        //
        // Returns:
        // List of words with their count printed to console.
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    string filepath = args[0];
                    var result = Parse(filepath);
                    Console.WriteLine("Top 10 Occurring Words");
                    foreach (var v in result)
                    {
                        Console.WriteLine(v.Key + " : \t" + v.Value);
                    }
                }
                else
                {
                    Console.WriteLine(""+args[0].ToString()+" file does not exist");
                }
            }
            else 
            {
                Console.WriteLine("usage: name input_file");
                //string filepath = "lord_of_rings.txt";
                //var result = Parse(filepath);
                //foreach (var v in result)
                //{
                //    Console.WriteLine(v.Key + " : \t" + v.Value);
                //}
            }
        }

        // Summary:
        //     Get punctuation and separators from ASCII character table.
        //
        // Returns:
        //     Array of punctuation and separator characters.

        public static Char[] getPunctuation()
        {
            Char[] P = new Char[] { };
            for (int i = 0; i < 256; i++)
            {
                Char c = (char)i;
                if (Char.IsPunctuation(c) || Char.IsSeparator(c))
                {
                    P.Append(c);
                }
            }
            return P;
        }

        // Summary:
        //     Get ten most occuring words from text file. 
        //     Create dictionary and increment key with reoccuring word.
        //
        // Parameters: 
        //     path of file containing the text. 
        //
        // Returns:
        //   Ten most occuring words as a dictionary object.
        public static IDictionary<string, int> Parse(string path)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            try
            {
                Char[] SP = getPunctuation();

                using (var streamReader = new StreamReader(path))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var words = line.ToLower().Split(SP, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var word in words)
                        {
                            if (wordCount.ContainsKey(word))
                            {
                                wordCount[word] += 1;
                            }
                            else
                            {
                                wordCount.Add(word, 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return wordCount.OrderByDescending(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
