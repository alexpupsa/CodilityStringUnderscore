using System.Text;

var testCases = new List<(string, string, string)>()
{
    ("valuevaluevalue","value","_valuevaluevalue_"),
    ("ABCDEF","A","_A_BCDEF"),
    ("AABCDEF","A","_AA_BCDEF"),
    ("ABCDEF","ABCGEFG","ABCDEF"),
    ("testthis is a testtest to see if testestest it works","test","_test_this is a _testtest_ to see if _testestest_ it works")
};

foreach(var testCase in testCases)
{
    Console.WriteLine($"s1: {testCase.Item1}");
    Console.WriteLine($"s2: {testCase.Item2}");
    Console.WriteLine($"expected: {testCase.Item3}");
    var result = GetStringWithSeparator(testCase.Item1, testCase.Item2, "_");
    Console.WriteLine($"actual: {result}");
    Console.WriteLine();
}

static string GetStringWithSeparator(string s1, string s2, string separator)
{
    var separatorIndexes = new List<int>();
    var foundSubstring = false;
    var s1Index = 0;
    var charactersToGo = 0;
    while (s1Index < s1.Length)
    {
        var startsWith = s1.Substring(s1Index).StartsWith(s2);
        if (startsWith)
        {
            if (!foundSubstring)
            {
                separatorIndexes.Add(s1Index);
            }
            foundSubstring = true;
            charactersToGo = s2.Length - 1;
        }
        else if (foundSubstring && charactersToGo < 0)
        {
            foundSubstring = false;
            separatorIndexes.Add(s1Index);
        }
        s1Index++;
        charactersToGo--;
    }
    if (foundSubstring)
    {
        separatorIndexes.Add(s1Index);
    }

    return BuildStringWithSeparators(s1, separatorIndexes, separator);
}

static string BuildStringWithSeparators(string s1, List<int> indexes, string separator)
{
    var sb = new StringBuilder();
    var s1Index = 0;
    foreach (var index in indexes)
    {
        sb.Append(s1[s1Index..index]);
        sb.Append(separator);
        s1Index += index - s1Index;
    }
    sb.Append(s1[s1Index..]);
    return sb.ToString();
}