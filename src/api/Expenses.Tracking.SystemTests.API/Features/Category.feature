@api
Feature: Category

A set of tests to verify /api/category endpoints

Scenario Outline: Create a category via POST request
	When I send a POST request to category endpoint with the following data
		| Name       |
		| <Category> |
	Then the response status code should be <StatusCode>

Examples:
	| Category      | StatusCode |
	| Post Category | 201        |
	| Post Category | 409        |
