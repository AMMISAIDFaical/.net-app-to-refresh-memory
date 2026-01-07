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
       var suggestions = new Dictionary<string, (int differenceCount, int length)>();


       // IMPORTANT: iterate over a copy to avoid modifying the collection
       foreach (var choice in new List<string>(choices))
       {
           if (choice.Length < term.Length)
           {
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
              
               suggestions[choice] = (differenceCount: differenceLettersCount,length: choice.Length);
           }
       }
       return suggestions
           .OrderBy(s => s.Value.differenceCount)
           .ThenBy(s => s.Value.length)
           .Take(numberOfSuggestions)
           .Select(s => s.Key)
           .ToList();
   }


  
}