# Unidays-Placement-Test
 
## How to build my program ###
The program I have made is written in C#.Net 7.0 and will be able to run on Visual Studio

## Approuch to the problem ###


## Classes and Other Improvements ##
Below, you will find a breif description of the main classes used in the program, as well as other important improvements:

#### Item Class ####
The item class is mainly used to store item information, such as item name, price and the type of discount it has. This class is mainly used for pricing rules, which is a dictionary that contains all the unique items that the shop has.

#### UnidaysDiscountChallenge Class ####
This class is used to store what items are currently in the basket and the pricing rules. The class also has 3 other methods, which is AddToBasket, GetBasket and CalculateTotalPrice.

##### Storing items in basket #####
The variable basket is a dictionary that has the item name as its key and the quantity of that item that is in the basket as its value. The reason why I have went for this approuch is the basket only needs to store essential information for the class, which is the item name (unique) and the quanitity of that item that is in the basket as an int. This allows the program to run more efficiently as instead of storing ```Basket = "AEACBCEECDDAB"```, you instead store ```Basket{["A"] = 3; ["B"] = 2, ["C"] = 2, ["D"] = 3}```. This allows CalculateTotalPrice to run more efficiently as you don't have to convert "AEACBCEECDDAB" to try to work out what the quantity of each item is. The class also has a parameter called PricingRules, which is a dictionary with all of the unique items that the store has. This used to get the price and discount type of a specific item.

#### Discount Class ####
The Discount class has one variable called promotional message, which contains the message intended to advertise the discount, such as "Buy 1 get 1 free". There are also another 2 classes that inherit from the Discount class, which is "QuantityForSetPrice" and "BuyQuantityGetQuantityFree". Both of these classes have another method called applyDiscount.

##### Discount approuch #####
When looking at the pricing rule online, I noticed that item B and C were very similar, and that D and E were also very similar. For example, the only difference between B and C is the item quanity and the new price. So based on this observation, I decided that I would only have 2 classes instead of having 4 classes for each discount. The program will then use the members of the class and parameters of the applyDiscount method to calculate the discounted price, instead of hardcoding each discount type. This means that Item B and Item C can use the same class code, but just with different parameters. Here's an example:

```
BDiscount = new QuantityGetQuantityFree("2 for £20", 2, 20); //promotionalMessage, purchaseQuantity, atPrice 
CDiscount = new QuantityGetQuantityFree("3 for £10", 3, 10);
BDiscountedPrice = BDiscount.ApplyDiscount(5, 12) //itemBasketQuantity, normalPrice
// Returns 52
CDiscount = CDiscount.ApplyDiscount(4, 4)
// Returns 14
```
