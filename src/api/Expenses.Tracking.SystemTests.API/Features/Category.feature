@api
Feature: Category

A set of tests to verify /api/category endpoints

Scenario: Create a category via POST request
	When I send a POST request to /api/category with the following data
		| Name			|
		| Test Category |
	Then the response status code should be 201
