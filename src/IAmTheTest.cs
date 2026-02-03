namespace Suggestions;


public interface IAmTheTest
{
   IEnumerable<string> GetSuggestions(
       string term,
       List<string> choices,
       int numberOfSuggestions);
}


public class MatchingWithTerm : IAmTheTest
{
   public IEnumerable<string> GetSuggestions(string term, List<string> choices, int numberOfSuggestions)
   {
       if (term is null)
       {
           throw new ArgumentNullException(nameof(term));
       }
       if (choices is null)
       {
           throw new ArgumentNullException(nameof(choices));
       }
       if (numberOfSuggestions < 0)
       {
           throw new ArgumentOutOfRangeException(nameof(numberOfSuggestions), "Must be zero or greater.");
       }

       Console.WriteLine($"[Matcher] Term: {term}");
       Console.WriteLine($"[Matcher] Initial choices count: {choices.Count}");
       Console.WriteLine($"[Matcher] Target suggestions: {numberOfSuggestions}");

       var suggestions = new Dictionary<string, (int differenceCount, int length)>();


       // IMPORTANT: iterate over a copy to avoid modifying the collection
       foreach (var choice in new List<string>(choices))
       {
           Console.WriteLine($"[Matcher] Checking choice: {choice}");
           if (choice.Length < term.Length)
           {
               Console.WriteLine($"[Matcher] Removing '{choice}' (shorter than term).");
               choices.Remove(choice);
           }
           else
           {
               int i = 0;
               int j = 0;
               int differenceLettersCount = 0;


               while (i < term.Length && j < choice.Length)
               {
                   if (term[i] != choice[j])
                   {
                       differenceLettersCount++;
                       i++;
                       j++;
                   }
                   else
                   {
                       i++;
                       j++;
                   }
               }
               
               Console.WriteLine($"[Matcher] Differences: {differenceLettersCount}, Length: {choice.Length}");
               suggestions[choice] = (differenceCount: differenceLettersCount, length: choice.Length);
           }
       }
       var result = suggestions
           .OrderBy(s => s.Value.differenceCount)
           .ThenBy(s => s.Value.length)
           .Take(numberOfSuggestions)
           .Select(s => s.Key)
           .ToList();
      
       Console.WriteLine($"[Matcher] Final suggestions: {string.Join(", ", result)}");
       return result;
   }
  
}
