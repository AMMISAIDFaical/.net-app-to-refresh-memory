using System;
using System.Collections.Generic;
using Suggestions;

    
class Program
{
   static void Main()
   {
       try
       {
           Console.WriteLine("Starting suggestions demo...");

           string term = "gros";
           List<string> choices = new List<string>()
           {
               "gros", "iras", "grag", "aggressif", "go", "iros", "gios", "grosgros"
           };
           int numberOfSuggestions = 3;

           Console.WriteLine($"Term: {term}");
           Console.WriteLine($"Number of suggestions: {numberOfSuggestions}");
           Console.WriteLine($"Choices ({choices.Count}): {string.Join(", ", choices)}");

           IAmTheTest matcher = new MatchingWithTerm();
           Console.WriteLine("Running matcher...");
           var suggestions = matcher.GetSuggestions(term, choices, numberOfSuggestions);

           Console.WriteLine("Suggestions:");
           foreach (var suggestion in suggestions)
           {
               Console.WriteLine($"- {suggestion}");
           }
       }
       catch (Exception ex)
       {
           Console.WriteLine($"Error: {ex.GetType().Name} - {ex.Message}");
       }
       finally
       {
           Console.WriteLine("Done.");
       }
   }
}
