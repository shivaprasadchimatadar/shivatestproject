using System;
using System.Collections.Generic; 

namespace PromotionEngine
{
    public class Program
    {
        public char skuID {get; set;}
        public int totalItems {get; set;}
        public int price {get; set;}
        public static void Main(string[] args)
        {
            // Unit Price for SkuIds
            SkuIdPrice('A', 50);
            SkuIdPrice('B', 30);
            SkuIdPrice('C', 20);
            SkuIdPrice('D', 15);

            // Active promotions
            BuildPromotionType1Dict('A', 3, 130);
            BuildPromotionType1Dict('B', 2, 45);
            BuildPromotionType2Dict('C', 'D', 30);

            // Scenario B
            Dictionary<char, int> items2 = new Dictionary<char, int>();
            items2.put('A', 5);
            items2.put('B', 5);
            items2.put('C', 1);
            Console.WriteLine("Answer for scenario B: " + CalculateTotalPrice(items2));

            totalPrice = 0; // resetting total price for new scenario

            // Scenario C
            Dictionary<char, int> items3 = new Dictionary<char, int>();
            items3.put('A', 3);
            items3.put('B', 5);
            items3.put('C', 1);
            items3.put('D', 1);
            Console.WriteLine("Answer for scenario C: " + CalculateTotalPrice(items3));           
            
        }

        Dictionary<char, Promotiontype1> pt1 = new Dictionary<char, Promotiontype1>();
        Dictionary<string, int> pt2 = new Dictionary<string, int>();
        Dictionary<char, int> currentPrice = new Dictionary<char, int>();
        
        public static void BuildPromotionType1Dict(char skuId,  int totalItems, int price){
            PromotionType1 promotionType1 = new PromotionType1();
            promotionType1.skuID = skuId;
            promotionType1.totalItems = totalItems;
            promotionType1.price = price;

            pt1.Add(skuId, promotionType1);

        }

        public static void BuildPromotionType2Dict(char skuId1, char skuId2, int price){
            string temp1 = skuId1 + skuId2;
            string temp2 = skuId2 + skuId1;

            pt2.Add(temp1, price);
            pt2.Add(temp2, price);

        }

        public static void SkuIdPrice(Character skuId, int price) {
         currentPrice.Add(skuId, price);
        }

        public static int CalculateTotalPrice(Dictionary<char, int> items) {
            // Prepare the set of SkudIds so that we can remove SkuIds from this set once the promotion is applied
            HashSet<char> skuIdSet = new HashSet<char>();
            foreach (KeyValuePair<char, int> key in items) { // This is foreach loop
                skuIdSet.Add(key);
            }
            // Apply promotion type 1
            foreach (KeyValuePair<char, int> key in items) { //iterating over map
                if (pt1.containsKey(key)) {
                    PromotionType1 promotionType1 = pt1.key;
                    computePromotion1(promotionType1, items.get(key), true, skuIdSet);
                }
            }

            // Apply promotion type 2
            foreach (KeyValuePair<char, int> key in items) {
                if (skuIdSet.contains(key)) {
                    computePromotion2(skuIdSet, key, items);
                }
            }

            // Remaining SkuIds where no promotion is applied
            foreach(char ch in skuIdSet) {
                totalPrice += (items.get(ch) * currentPrice.get(ch));
            }

            return totalPrice;
        }

        public static void computePromotion1(PromotionType1 promotionType1, int customerOrder, bool isFirst, HashSet<char> skuIdSet) {
            if (customerOrder >= promotionType1.getTotalItems()) {
            // Remove from the set as the promotion is applied on this skuId
            if (skuIdSet.contains(promotionType1.getSkuId())) {
            skuIdSet.Remove(promotionType1.getSkuId());
            }
            totalPrice += promotionType1.getPrice();
            customerOrder -= promotionType1.getTotalItems();
            computePromotion1(promotionType1, customerOrder, false, skuIdSet);
            } else {
            if (isFirst) {
            return;
            } else {
            totalPrice += customerOrder * currentPrice.get(promotionType1.getSkuId());
            }
            }
        }

        public static void computePromotion2(HashSet<Character> skuIdSet, Character key, Dictionary<char, int> items) {
            foreach (char ch in skuIdSet) {
                String temp = String.valueOf(key) + String.valueOf(ch);
                    if (pt2.containsKey(temp)) {
                    int minimumItems = Math.min(items.get(ch), items.get(key));
                    totalPrice += (pt2.get(temp) * minimumItems);

                    totalPrice += ((items.get(key) - minimumItems) * currentPrice.get(key));
                    totalPrice += ((items.get(ch) - minimumItems) * currentPrice.get(ch));

                    skuIdSet.Remove(ch);
                    skuIdSet.Remove(key);
                    break;
                }
            
            }
        }
    }
}
