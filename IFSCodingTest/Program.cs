// See https://aka.ms/new-console-template for more information

/*
It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements. For example:
*/

using System.Text.RegularExpressions;

char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
string[] noIncludes = { "ab", "cd", "pq", "xy" };

bool IsValidString(string str)
{
    // Minimum length of the string must be 4, since it must include 3 vowels and at least double letter where double character can be a vowel too.
    if (str != null && str.Length >= 4)
    {
        var len = str.Length;
        int doubleLetterCount = 0;

        // This is used to check the distinct vowel count.
        // This data structure contains no duplicate elements.
        HashSet<char> stringVowels = new();

        // Check whether the last letter is a vowel because iteration goes for (last index - 1).
        var isLastLetterIsVowel = vowels.Contains(str[len - 1]);
        if (isLastLetterIsVowel)
            stringVowels.Add(str[len - 1]);

        // Iterate through array for (last index - 1) since it checks with next letter.
        for (int i = 0; i < len - 1; ++i)
        {
            var letter = str[i];
            var nextLetter = str[i + 1];
            var doubleLetter = new string(new char[] { letter, nextLetter });

            // Check the existence of ab, cd, pq, xy. 
            if (noIncludes.Any(n => n.Equals(doubleLetter)))
                return false;

            // Check the vowel count.
            if (vowels.Contains(letter))
                stringVowels.Add(letter);

            // Check the double letter count such as aa, bb, cc..etc..
            if (letter.Equals(nextLetter))
                ++doubleLetterCount;
        }

        if (doubleLetterCount >= 1 && stringVowels.Count() >= 3)
            return true;
    }

    return false;
}

// This is another approach to find the valid string using regular expressions.
bool IsValidStringRegExMatch(string str)
{
    if (str != null && str.Length >= 4)
    {
        var identicalCharMatch = Regex.IsMatch(str, @"(.)\1");
        var noIncludesMatch = Regex.IsMatch(str, @"^\bab|cd|pq|xy\b");

        if (identicalCharMatch && !noIncludesMatch)
        {
            var vowelMatch = Regex.Matches(str, @"^a|e|i|o|u").OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .Distinct();

            if (vowelMatch.Count() >= 3)
                return true;
        }
    }

    return false;
}

var testCases = new List<string> { null, string.Empty, "aeei", "aaee", "aaeei", "aabeei", "aiko", "aibba", "aibbe", "akbio", "abnii", "abniiu", "abniiukk", "agniiukk", "agniiukkxy", "ugknbfddgicrmopn", "ugkn bfddgicrmopn", "ugknbfd dgicrmopn" };
foreach (var text in testCases)
{
    var valid = IsValidString(text);
    //var valid = IsValidStringRegExMatch(text);
    var validity = valid ? "valid" : "invalid";
    Console.WriteLine($"{text} is a {validity} string.");
}
