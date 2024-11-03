Feature: Category

This feature will check category UI web page

@tag1
Scenario: Load category page
	Given The ExpensesUi page is loaded
	When The support button is clicked
	Then The category table is displayed
