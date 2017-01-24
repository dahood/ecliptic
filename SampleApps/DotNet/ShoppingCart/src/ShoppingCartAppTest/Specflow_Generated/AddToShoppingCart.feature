
Feature: ShoppingCartFeature

Scenario: Simple Checkout One Item
Given I start with an Empty Shopping Cart 

And I have the following Products to sell
| SKU | Name | Unit Price |
| 001 | Milk | 2.5        |
| 002 | Eggs | 3.50       |

And I add Milk to the Shopping Cart 

When I go to checkout 

Then I should have the following in the shopping Cart
| SKU | Item | Price |
| 001 | Milk | 2.50  |

And my total should be 2.50 

Scenario: Simple Checkout Multiple Items
Given I start with an Empty Shopping Cart 

And I have the following Products to sell
| SKU | Name | Unit Price |
| 001 | Milk | 2.50       |
| 002 | Eggs | 3.50       |

And I add Milk to the Shopping Cart 

And I add Eggs to the Shopping Cart 

When I go to checkout 

Then I should have the following in the shopping Cart
| SKU | Item | Price |
| 001 | Milk | 2.50  |
| 002 | Eggs | 3.50  |

And my total should be 6.00 

