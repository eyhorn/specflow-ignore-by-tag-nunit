Feature: Calculations
	In order to perform basic calculations
    As a user of this program
    I want to be told results of calculations


Scenario Outline: Perform basic operations
	Given I have provided <First Number> and a <Second Number>
	When I perform operation '<Operation>'
	Then the result should be <Result Number>

Examples: 
    | Operation | First Number | Second Number | Result Number |
    | Add       | 5            | 7             | 12            |
    | Add       | 3            | 4             | 7             |
    | Sub       | 4            | 1             | 3             |
    | Sub       | 987          | 342           | 645           |
    | Multiply  | 7            | 3             | 21            |
    | Multiply  | 14           | 12            | 168           |
    | Divide    | 121          | 11            | 11            |
    | Divide    | 63           | 7             | 9             |
    
@Superseded
Scenario: Perform calculations
    Given An outdated test
    When I perform outdated operation
    Then Some Result will be produced
    