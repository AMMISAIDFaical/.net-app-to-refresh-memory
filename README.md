# Term Suggestions (C# / .NET)

A small .NET console app that demonstrates a simple “term suggestions” matcher: given an input `term`, a list of candidate `choices`, and a maximum number of results, it returns the best matches based on a lightweight character-by-character difference score.

## What it does

The included matcher (`MatchingWithTerm`) implements the `IAmTheTest` interface and:

1. Filters out candidates shorter than the input term.
2. Scores each remaining candidate by counting how many characters differ at the same index for the first `term.Length` characters.
3. Sorts candidates by:
	 - lowest difference score first
	 - then shortest candidate length
4. Returns the top `numberOfSuggestions` results.

In other words, it’s a strict, index-based comparison (not edit-distance / Levenshtein). It’s fast and easy to understand, but not as forgiving as true fuzzy matching.

## Prerequisites

- .NET SDK capable of building `net10.0`
	- In GitHub Codespaces/devcontainers, the SDK is typically preinstalled.

Check your SDK:

```bash
dotnet --info
```

## Run

From the repo root:

```bash
dotnet run --project src/dotnet-codespaces.csproj
```

Expected output format:

```text
Suggestions:
<suggestion 1>
<suggestion 2>
...
```

To change the input values, edit `src/Program.cs`:

- `term` – the user input
- `choices` – candidate strings
- `numberOfSuggestions` – number of results to return

## Build

```bash
dotnet build src/dotnet-codespaces.csproj
```

## How the matcher is structured

The matching contract is the `IAmTheTest` interface:

```csharp
IEnumerable<string> GetSuggestions(string term, List<string> choices, int numberOfSuggestions);
```

The current implementation is `MatchingWithTerm`. If you want to try a different strategy (e.g., prefix matching, substring matching, edit distance, phonetics), create a new class that implements `IAmTheTest` and swap it in within `Program.cs`.

## Notes and limitations (current behavior)

- Candidates shorter than `term` are removed before scoring.
- The score counts only same-position mismatches for the first `term.Length` characters.
- Sorting uses “fewest mismatches” and then “shortest length”.
- The matcher currently mutates the provided `choices` list (it removes short entries). If you want pure/non-mutating behavior, pass a copy of the list into `GetSuggestions`.

## Repository layout

- `src/Program.cs` — console entrypoint and example usage
- `src/IAmTheTest.cs` — interface and current matcher implementation
- `src/dotnet-codespaces.csproj` — project file (`net10.0`)

## Troubleshooting

### Target framework not supported

If you see errors about `net10.0`, install a .NET SDK that supports it, or change `<TargetFramework>` in `src/dotnet-codespaces.csproj` to a framework you have installed (for example `net8.0`), then rebuild.

### No suggestions returned

Verify that your `choices` list contains strings with length >= `term.Length`, and that `numberOfSuggestions` is greater than 0.

