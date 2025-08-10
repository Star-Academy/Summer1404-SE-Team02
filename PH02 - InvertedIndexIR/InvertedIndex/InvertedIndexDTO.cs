using System;
using System.Collections;

public class InvertedIndex 
{
    public Dictionary<string, List<KeyValuePair<string, int>>> wordDocMap = new Dictionary<string, List<KeyValuePair<string, int>>>();
    public HashSet<string> documentNames = new HashSet<string>();
}