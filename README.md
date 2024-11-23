# BudgetAppV2.2
This helps me keep track of taxes, earnings, and expenditures.
Budget App, Version 2.2
Ethan Vaughan

This budget app allows me to record expenditures. 
From the start menu, I can enter a new financial transaction. I specify a date for the transaction, a description of the transaction, a transaction amount (in dollars and cents), and a category. (The category comb box has one additional field – “Other Earnings” – that is used to record any non-wage earning, such as finding some cash, or returning an item to the store and getting my money back.)

On the right side of the start menu screen is a table that shows the last five expenditures that I entered in the system.

I can view a year-to-date report on my expenditures by clicking on “Budget Report” from the start menu. This will show me how much money I’ve spent in all the different spending categories. I can change the start/end dates and get notices on the amount of money spent between two dates (inclusive). By default, the start date is January 1st of the current year, and the end date is the current day.

The database itself contains extra tables for taxation, tax returns/liabilities, and gross wages for each job I’ve worked. This is not reflected in the GUI because at the end of the year I will go to the database and manually enter this information. 

To install this program, you need to be using a Windows computer and have Visual Studio. Run the createDatabase SQL script in SQLite.