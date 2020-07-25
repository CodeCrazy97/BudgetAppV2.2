# Below query shows the net earnings (from working jobs).
SELECT wage_year AS 'Year', SUM(amount) AS 'Total Salary'
FROM gross_wages 
GROUP BY wage_year;

# Below query shows the total taxable earnings from jobs by year as well as the total taxes paid for working those jobs.
# The tax rate is given (the tax return is not included in the rate).
SELECT gw.wage_year, ROUND(SUM(gw.amount), 0) AS 'Total Earnings', ROUND(IFNULL(total_tax.Taxes_Paid, 0) + IFNULL(tl.amount, 0), 0) AS 'Taxes Paid', ROUND(((IFNULL(tl.amount, 0) + IFNULL(total_tax.Taxes_Paid, 0) - IFNULL(tr.amount, 0))/SUM(gw.amount))*100, 2) AS 'Tax Rate'
FROM gross_wages gw
LEFT JOIN (
SELECT transaction_year, SUM(amount) AS 'Taxes_Paid'
FROM taxation
GROUP BY transaction_year) total_tax ON gw.wage_year = total_tax.transaction_year
LEFT JOIN (
SELECT tax_year, SUM(amount) AS 'amount'
FROM tax_return
GROUP BY tax_year) tr ON tr.tax_year = gw.wage_year
LEFT JOIN (
SELECT tax_year, SUM(amount) AS 'amount'
FROM tax_liability
GROUP BY tax_year) tl ON tl.tax_year = gw.wage_year
GROUP BY gw.wage_year;

# Below query shows grand total earnings by year.
SELECT gw.wage_year AS 'Year', SUM(gw.amount) + IFNULL(SUM(tr.amount), 0) + IFNULL(SUM(oe.amount), 0) AS 'Grand Total Earnings'
FROM gross_wages gw
LEFT JOIN (
SELECT tax_year, SUM(amount) AS 'amount'
FROM tax_return
GROUP BY tax_year) tr ON tr.tax_year = gw.wage_year
LEFT JOIN (
SELECT YEAR(earning_date) AS 'year', SUM(amount) AS 'amount'
FROM other_earnings
GROUP BY YEAR(earning_date)) oe ON oe.year = gw.wage_year
GROUP BY gw.wage_year;
