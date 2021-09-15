using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using Utils;

namespace String
{
    public class EnglishDictionary
    {
        public static TST<bool> Load()
        {
            string dictionaryFile = Util.DocumentPath + "words_alpha.txt";
            string[] words = File.ReadAllLines(dictionaryFile);
            TST<bool> dict = new TST<bool>();
            foreach (string word in words)
                dict.Put(word, true);
            return dict;
        }
    }
}
