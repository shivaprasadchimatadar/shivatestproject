using System;

namespace PromotionEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BuildPromotionType1Map();
            
        }


        Dictionary<char, Promotiontype1> pt1 = new Dictionary<char, Promotiontype1>();
        Dictionary<string, int> pt2 = new Dictionary<string, int>();
        Dictionary<char, int> currentPrice = new Dictionary<char, int>();
        
        public static void BuildPromotionType1Dict(char skuId, Promotiontype1 promotiontype1){
            pt1.Add(skuId, promotiontype1);

        }
        public static void BuildPromotionType2Dict(char skuId1, char skuId2, int price){
            string temp1 = skuId1 + skuId2;
            string temp2 = skuId2 + skuId1;

            pt2.Add(temp1, price);
            pt2.Add(temp2, price);

        }

        public static void SkuIdPrice(char skuId, int price){
            currentPrice.Add(skuId, price);
        }

        public static int CalculateTotalPrice(Dictionary<char, int> items){

        
        }
    }
}
