using System;

namespace PromotionEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            BuildPromotionType1Map();
            
        }


        Dictionary<char, Promotiontype1> pt1 = new Dictionary<char, Promotiontype1>();
    
        
        public static void BuildPromotionType1Dict(char skuId, Promotiontype1 promotiontype1){
            pt1.Add(skuId, promotiontype1);

        }

    }
}
