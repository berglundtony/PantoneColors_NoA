# PantoneColors_NoA
ASP.NET MVC Core project getting information from external API and present it in different lists.

The following document lists the requirements for a test where colors are fetched from an online API and grouped according to a described ruleset.

 - Fetch all pages of items from the following JSON endpoint, and merge them into one list. https://reqres.in/api/example?per_page=2&page=1 

 

 Use the "page" querystring to fetch a specific page. Do not remove or change the "per_page" parameter. 

 - The list should be divided into three new lists based on the following rules: 

 * Group 1: Based on the first part of the "pantone_value" field (e.g. "17" of "17-4587"). If this value is divisible by 3 (with no remainders) then the item should be included in this group.

 * Group 2: Based on the first part of the "pantone_value" field (e.g. "17" of "17-4587"). If this value is divisible by 2 (with no remainders) AND it is not included in group 1 then the item should be included in this group. 

 * Group 3: Include any items that does not fit the criteria of group 1 or 2. 

 * Present the three lists sorted by the "year" field. 
 Note: 
 o The solution should be built using ASP.NET MVC. 
 o Choose any fitting presentation layer that can visualize the data. The UI does not need to be beautiful. 
 o Push the source code to your own private Git repository or ask us to create a repo for you in our Github organization. 
 o The task is designed to take at max a few hours of your time, do not feel pressured to spend more time than this on architecture and design.
