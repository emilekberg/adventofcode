using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020
{
	public record Dish(List<string> Ingredients, List<string> Allergens)
	{
		public bool HasIngredient(string ingredient)
		{
			return Ingredients.Contains(ingredient);
		}
	}
	public class Day21 : BaseDay<string[], int>, IDay
	{
		public string DangerousList { get; private set; }
		private Dictionary<string, string> IngredientsWithAllergens { get; set; } = new();
		public override int Part1(string[] input)
		{
			var dishes = input.Select(row =>
			{
				var split = row.Split("(contains ", StringSplitOptions.RemoveEmptyEntries);
				var ingredients = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
				var allergens = split[1].Replace(")", string.Empty).Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

				return new Dish(ingredients, allergens);
			}).ToList();

			var allIngredients = dishes.SelectMany(x => x.Ingredients).Distinct().ToList();
			var safeIngredients = allIngredients.ToList();

			var ingredientsPotentialAllergens = new Dictionary<string, List<string>>();
			foreach (var dish in dishes)
			{
				foreach (var ingredient in dish.Ingredients)
				{
					ingredientsPotentialAllergens.TryAdd(ingredient, new List<string>());
					ingredientsPotentialAllergens[ingredient].AddRange(dish.Allergens);
				}
			}
			Dictionary<string, List<string>> previousState = null;
			// Loop the algorithm below until we cannot identify any other allergens.
			do
			{
				previousState = ingredientsPotentialAllergens.ToDictionary(x => x.Key, x => x.Value.ToList());
				var toRemove = new List<string>();
				foreach (var ingredient in safeIngredients)
				{
					string highestAllergen = string.Empty;
					// creates a list of allergenes and their count for this ingredient.
					var lookup = ingredientsPotentialAllergens[ingredient].GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
					// check if any other ingredient contains more of this allergen, if so, that would be a better candidate for a potential issue.
					foreach (var (key, value) in ingredientsPotentialAllergens)
					{
						if (ingredient == key) continue;
						foreach (var (allergen, something) in lookup)
						{
							var count = value.Count(x => x == allergen);
							// if a candidate with higher count of the allergen is found, we should remove this allergen from the lookup, because we cannot trust it.
							if (count >= lookup[allergen])
							{
								lookup.Remove(allergen);
							}
						}
					}

					// if lookup is empty, that means this is not safe to assume for it to be an allergen.
					if (lookup.Count == 0)
					{
						continue;
					}

					// if any allergen remains in lookup, remove ingredient from safe list and remove allergen from other products.
					toRemove.Add(ingredient);
					ingredientsPotentialAllergens.Remove(ingredient);
					foreach (var (key, value) in ingredientsPotentialAllergens)
					{
						foreach (var (allergen, something) in lookup)
						{
							IngredientsWithAllergens.TryAdd(ingredient, allergen);
							value.RemoveAll(x => x == allergen);
						}
					}
				}
				safeIngredients = safeIngredients.Except(toRemove).ToList();
			} while (previousState.Count != ingredientsPotentialAllergens.Count);
			


			 return dishes.Sum(x => x.Ingredients.Intersect(safeIngredients).Count());
		}

		public override int Part2(string[] input)
		{
			Part1(input);
			DangerousList = string.Join(",", IngredientsWithAllergens.OrderBy(x => x.Value).Select(x => x.Key));
			Console.WriteLine(DangerousList);
			return 0;
		}
	}
}
 