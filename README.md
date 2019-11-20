# Unidays-Placement-Test
 
### How to build my program ###
The program I have made is written in C#.Net 7.0 and will be able to run on Visual Studio

### Approuch to the problem ###
My program has 3 main classes:
- `Item`
- `ShoppingBasket`
- `Discount`

#### Item Class ####
The item class is mainly used to store item information, such as item name, price and the type of discount it has. This class is mainly used for pricing rules, which is a dictionary that contains all the unique items that the shop has.

#### ShoppingBasket Class ####
This class is used to store what items are currently in the basket and the pricing rules. The class also has 3 other methods, which is AddToBasket, GetBasket and CalculateTotalPrice.

##### Storing items in basket #####
The variable basket is a dictionary that has the item name as its key and the quantity of that item that is in the basket as its value. The reason why I have went for this approuch is the basket only needs to store essential information for the class, which is the item name (unique) and the quanitity of that item that is in the basket as an int. This allows the program to run more efficiently as instead of storing ```Basket = "AEACBCEECDDAB"```, you instead store ```Basket{["A"] = 3; ["B"] = 2, ["C"] = 2, ["D"] = 3}```. This allows CalculateTotalPrice to run more efficiently as you don't have to convert "AEACBCEECDDAB" to try to work out what the quantity of each item is. The class also has a parameter called PricingRules, which is a dictionary with all of the unique items that the store has. This used to get the price and discount type of a specific item.
