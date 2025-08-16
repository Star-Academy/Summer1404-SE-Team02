using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;

public class InvertedIndex 
{
    public Dictionary<string, LinkedList<KeyValuePair<string, int>>> invertedIndex = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();
    public HashSet<string> documentNames = new HashSet<string>();
}