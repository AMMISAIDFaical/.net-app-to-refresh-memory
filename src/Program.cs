using System;
using System.Collections.Generic;
using Suggestions;

    
class Program
{
   static void Main()
   {
       string term = "gros";
       List<string> choices = new List<string>()
       {
           "gros", "iras", "grag", "aggressif", "go", "iros", "gios", "grosgros"
       };
       int numberOfSuggestions = 3;
       IAmTheTest matcher = new MatchingWithTerm();
       var suggestions = matcher.GetSuggestions(term, choices, numberOfSuggestions);
       Console.WriteLine("Suggestions:");
       foreach (var suggestion in suggestions)
       {
           Console.WriteLine(suggestion);
       }
   }
}