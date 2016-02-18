using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAutoCorrectPad
{
    public partial class MainForm : Form
    {
        List<string> dictionaryWords;
        bool textChangedByUser = true;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dictionaryWords = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "TextAutoCorrectPad.Resources.WordsDictionary.txt";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    dictionaryWords.Add(reader.ReadLine());
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChangedByUser)
            {
                int cursorPos = textBox1.SelectionStart;
                string textBeforeCursor = textBox1.Text.Substring(0, cursorPos);

                if (textBox1.Text.Length > 0)
                {
                    if (char.IsWhiteSpace(textBeforeCursor.Last()))
                    {
                        int posOfLastWhiteSpace = -1;
                        for (int i = cursorPos - 3; i >= 0; i--)
                        {
                            if (char.IsWhiteSpace(textBeforeCursor[i]))
                            {
                                posOfLastWhiteSpace = i;
                                break;
                            }
                        }
                        int beginingOfWord = posOfLastWhiteSpace + 1;
                        int endingOfWord = cursorPos - 1;
                        string lastWrittenWord = textBox1.Text.Substring(beginingOfWord, endingOfWord - beginingOfWord);
                        bool endsWithWhiteSpace = char.IsWhiteSpace(lastWrittenWord.Last());
                        if (endsWithWhiteSpace)
                        {
                            lastWrittenWord = lastWrittenWord.Substring(0, lastWrittenWord.Length - 1);
                            endingOfWord--;
                        }

                        string bestMatch = BestMatch(lastWrittenWord);
                        int newCursorPos = beginingOfWord + bestMatch.Length + 1;
                        if (endsWithWhiteSpace) newCursorPos++;

                        textChangedByUser = false;
                        textBox1.Text = textBox1.Text.Substring(0, beginingOfWord) +
                                        bestMatch +
                                        textBox1.Text.Substring(endingOfWord);
                        textChangedByUser = true;

                        textBox1.SelectionStart = cursorPos;
                        textBox1.SelectionLength = 0;
                    }
                }
            }
        }

        private string BestMatch(string wordForMatching)
        {
            List<int> distances = new List<int>();
            int minDistance = wordForMatching.Length * 3;
            foreach (var word in dictionaryWords)
            {
                int curDist = LevenshteinDistance(word, wordForMatching.ToLower());
                if (curDist == 0) return wordForMatching;
                distances.Add(curDist);
                if (minDistance > curDist) minDistance = curDist;
            }

            List<string> bestMaches = new List<string>();
            for (int i = 0; i < distances.Count; i++)
            {
                if (distances[i] == minDistance) bestMaches.Add(dictionaryWords[i]);
            }

            if (bestMaches.Count == 1) return bestMaches[0];
            else
            {
                List<int> numberOfSameChars = new List<int>(bestMaches.Count);
                int maxSameChars = 0;
                foreach (var word in bestMaches)
                {
                    int sameCharsNum = 0;
                    int lowestLenght = word.Length < wordForMatching.Length ? word.Length : wordForMatching.Length;
                    for (int i = 0; i < lowestLenght; i++)
                    {
                        if (wordForMatching[i] == word[i]) sameCharsNum++;
                    }
                    numberOfSameChars.Add(sameCharsNum);
                    if (maxSameChars < sameCharsNum) maxSameChars = sameCharsNum;
                }

                for (int i = 0; i < numberOfSameChars.Count; i++)
                {
                    if (numberOfSameChars[i] == maxSameChars) return bestMaches[i];
                }
                return bestMaches[0];
            }
        }

        private int LevenshteinDistance(string word1, string word2)
        {
            int n = word1.Length;
            int m = word2.Length;
            int[,] distanceMatrix = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    int substitutionCost;
                    if (word1[i - 1] == word2[j - 1]) substitutionCost = 0;
                    else substitutionCost = 1;
                    distanceMatrix[i, j] = Min(distanceMatrix[i - 1, j] + 1,    //deletion
                                               distanceMatrix[i, j - 1] + 1,    //insertion
                                               distanceMatrix[i - 1, j - 1] + substitutionCost); //substitution
                }
            }

            return distanceMatrix[word1.Length, word2.Length];
        }

        private static int Min(int a, int b, int c)
        {
            int min = a;
            if (min > b) min = b;
            if (min > c) min = c;
            return min;
        }
    }
}