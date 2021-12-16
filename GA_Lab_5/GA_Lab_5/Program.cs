using System;
using System.Collections.Generic;
using System.Linq;

namespace GA_Lab_5
{
	class Program
	{

		static void Main(string[] args)
		{
			var cultureList = Data.Cultures;
			var startPopulation = GenerateStartPopulation();

			var winner = StartGenAlg(cultureList, startPopulation);

			WriteInfo(cultureList, winner);
		}

		public static int[] StartGenAlg(List<Culture> cultureList, int[][] startPopulation)
		{
			int[] winner = null;

			int[] tempWinner = null;

			var tempCash = 0;

			var tempPopulation = startPopulation;

			var count = 0;

			while((winner != null && tempWinner != null && 
				(CalcProfit(cultureList, winner) >= CalcProfit(cultureList, tempWinner) ||
				CalcProfit(cultureList, tempWinner) < Data.MinProfit || 
				CalcCosts(cultureList, tempWinner) > Data.MaxMoney)) || 
				winner == null || tempWinner == null)
			{
				if (count >= Data.MaxGenerationCount)
				{
					tempWinner = null;
					break;
				}

				if (winner != null)
				{
					tempCash = CalcProfit(cultureList, winner);
					tempWinner = winner;
				}

				tempPopulation = tempPopulation.OrderByDescending(gen =>
					{
						return gen.Select((x, i) =>
						{
							int profitSum = 0;

							profitSum = (cultureList[gen[i]].Specs[i] * Data.MaxKg) / 100; 

							return profitSum;
						}).Sum();
					}).ToArray();

				winner = tempPopulation.First();

				var selectionPopulation = SelectionPopulation(cultureList, startPopulation);

				var populationAfterCrossover = Crossover(selectionPopulation);

				tempPopulation = Mutation(populationAfterCrossover);

				count++;
			}

			Console.WriteLine($"Количество итераций: {count}");

			return tempWinner;
		}

		public static int CalcCosts(List<Culture> cultureList, int[] gen)
		{
			var sum = 0;

			for (int i = 0; i < gen.Length; i++)
			{
				sum += cultureList[gen[i]].PurchasePrice;
			}

			return sum;
		}

		public static int CalcProfit(List<Culture> cultureList, int[] gen)
		{
			var sum = 0;

			for (int i = 0; i < gen.Length; i++)
			{
				sum += (cultureList[gen[i]].Specs[i] * Data.MaxKg )/100;	
			}
			return sum;
		}

		public static int[][] Mutation(int[][] population)
		{
			var rnd = new Random();

			foreach (var gen in population)
			{
				var randIndex = rnd.Next(0, gen.Length);

				gen[randIndex] = gen[randIndex] < Data.Cultures.Count-3
					? rnd.Next(0, Data.Cultures.Count)
					: 0;

			}

			return population;
		}

		public static int[][] SelectionPopulation(List<Culture> cultureList, int[][] population)
		{
			return population.OrderByDescending(gen => gen.Select((x, i) =>
			{
				int sum = 0;

				for (int j = 0; j < gen.Length; j++)
					sum += (int)Math.Pow((cultureList[gen[i]].Specs[i] * Data.MaxKg) / 100, 2);

				return sum;
			}).Sum()).Take(5).ToArray();
		}

		public static int[][] Crossover(int[][] population)
		{
			var rnd = new Random();

			var resultCrossover = new List<int[]>();

			for (int i = 0; i < population.Length; i++)
			{
				var gen1 = population[rnd.Next(0, population.Length)];
				var gen2 = population[rnd.Next(0, population.Length)];

				var newGen = new List<int>();

				for (int j = 0; j < gen1.Length; j++)
				{
					switch (j)
					{
						case 0 or 1:
							newGen.Add(gen1[j]);
							break;
						case 2 or 3:
							newGen.Add(gen2[j]);
							break;
						case 4 or 5:
							newGen.Add(gen1[j]);
							break;
					}
				}

				resultCrossover.Add(newGen.ToArray());
			}

			return population.Union(resultCrossover).ToArray();
		}

		public static void WriteInfo(List<Culture> cultureList, int[] winner)
		{
			if (winner is null)
			{

				Console.WriteLine("Что-то не так");

				return;
			}

			for (int p = 0 ; p < winner.Length; p++)
			{
				Console.WriteLine($"Поле {p}: {cultureList[winner[p]].Name} ");
			}

			Console.WriteLine($"Затраты: {CalcCosts(cultureList, winner)} (max: {Data.MaxMoney})");
			Console.WriteLine($"Собранный урожай(кг): {CalcProfit(cultureList, winner)} (min: {Data.MinProfit})");
		}

		public static int[][] GenerateStartPopulation()
		{
			var rnd = new Random();
			var population = new List<int[]>();

			for (int i = 0; i < 10; i++)
				population.Add(new int[] { rnd.Next(0, Data.Cultures.Count), rnd.Next(0, Data.Cultures.Count), rnd.Next(0, Data.Cultures.Count), rnd.Next(0, Data.Cultures.Count), rnd.Next(0, Data.Cultures.Count) });

			return population.ToArray();
		}


	}
}
