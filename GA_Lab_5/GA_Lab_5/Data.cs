using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_Lab_5
{
    public class Data
    {
        public readonly static int MinProfit = 50;

        public readonly static int MaxMoney = 1000;
		public readonly static int MaxKg = 100;

		public readonly static int MaxGenerationCount = int.MaxValue / 10000;
		
		public static List<Culture> Cultures = new List<Culture>()
		{
			new Culture {   Name = "Картофель", PurchasePrice = 130, Specs = new int[] { 83, 52, 18, 2, 57 }  },
			new Culture{ Name = "Кукуруза", PurchasePrice = 55,  Specs = new int[] { 50, 22, 71, 63, 76 } },
			new Culture { Name = "Рожь", PurchasePrice = 60,   Specs = new int[] { 59, 44, 21, 71, 58 }},
			new Culture{Name = "Овес", PurchasePrice = 90,   Specs = new int[] { 95, 79, 57, 88, 64 }},
			new Culture{Name = "Арбузы", PurchasePrice = 20,     Specs = new int[] { 76, 81, 83, 59, 11 }},
			new Culture{Name = "Пшеница", PurchasePrice = 45,     Specs = new int[] { 47, 54, 21, 59, 65 }},
			new Culture{Name = "Дыни", PurchasePrice = 200,     Specs = new int[] { 2, 10, 3, 40, 78 }}


		};
    }
}
