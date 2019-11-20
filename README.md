## How to build my program ###
The program I have made is written in C#.Net 7.0 and will be able to run on Visual Studio

## Approach to the problem ##
The first step I did was note down all the requirements that the program needs to meet. Next, I look through the requirements and start thinking about what classes and methods I will need and how I will store variables, such as what data type to use and if I should use an array, list or dictionary. I also try to look for some similarities between the requirements, which can help with identifying ways of improving the efficiency of the program. I then created a new project and add it to this repository, while making sure that I commit after each change. I then start going through the requirements list and working on one requirement at a time. While programming, if I identify another potential way of improving the program, I send a new commit and start working the new idea I have to improve the program. If the code starts to run into issues due to a major change, I can always revert to a previous commit.

## Classes and Other Improvements ##
Below, you will find a brief description of the main classes used in the program, as well as other important improvements:

#### **Item Class** ####
The item class is mainly used to store item information, such as item name, price and the type of discount it has. This class is mainly used for pricing rules, which is a dictionary that contains all the unique items that the shop has.

#### **UnidaysDiscountChallenge Class** ####
This class is used to store what items are currently in the basket and the pricing rules. The class also has 3 other methods, which is AddToBasket, GetBasket and CalculateTotalPrice.

##### Storing items in basket #####
The variable basket is a dictionary that has the item name as its key and the quantity of that item that is in the basket as its value. The reason why I have gone for this approach is the basket only needs to store essential information for the class, which is the item name (unique) and the quantity of that item that is in the basket as an int. This allows the program to run more efficiently as instead of storing ```Basket = "AEACBCEECDDAB"```, you instead store ```Basket{["A"] = 3; ["B"] = 2, ["C"] = 2, ["D"] = 3}```. This allows CalculateTotalPrice to run more efficiently as you don't have to convert "AEACBCEECDDAB" to try to work out what the quantity of each item is.

#### **Discount Class** ####
The Discount class has one variable called promotional message, which contains the message intended to advertise the discount, such as "Buy 1 get 1 free". There are also another 2 classes that inherit from the Discount class, which is "QuantityForSetPrice" and "BuyQuantityGetQuantityFree". Both of these classes also have a method called applyDiscount.

##### Discount approach #####
When looking at the pricing rule on task, I noticed that item B and C were very similar and that D and E were also very similar. For example, the only difference between B and C is the item quantity and the new price. So based on this observation, I decided that I would only have 2 classes instead of having 4 classes for each discount. The program will then use the members of the class and parameters of the applyDiscount method to calculate the discounted price, instead of hardcoding each discount type. This means that Item B and Item C can use the same class code, but just with different parameters. Here's an example:

```
BDiscount = new QuantityGetQuantityFree("2 for £20", 2, 20); //promotionalMessage, purchaseQuantity, atPrice 
CDiscount = new QuantityGetQuantityFree("3 for £10", 3, 10);
BDiscountedPrice = BDiscount.ApplyDiscount(5, 12) //itemBasketQuantity, normalPrice
// Returns 52
CDiscountPrice = CDiscount.ApplyDiscount(4, 4)
// Returns 14
```
