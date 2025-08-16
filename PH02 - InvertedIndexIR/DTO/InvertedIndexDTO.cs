using System;
using System.Collections;

public class InvertedIndex 
{
    public Dictionary<string, List<KeyValuePair<string, int>>> WordDocMap = new Dictionary<string, List<KeyValuePair<string, int>>>();
    public HashSet<string> DocumentNames = new HashSet<string>();
}