Can calculate leap year rules up to 1000, with up to 3 rules, takes 2-3 minutes on Ryzen 9 5900x
This works by bruteforcing the rules for the year
### NOTE
if the Real year length is closer to the leap year length than the normal year length, It will output reversed rules
* **ex.1**, if real year length is 298.8834, leap year is 299, and normal is 298 <br />
So **reversed** output <br />
the output would be 163 498 9 <br />
the 9 meaning if year is divisible by 9, it is **NOT** leap year <br />
the 163 and the 498 meaning if year is divisible by 163 but **NOT** by 498, it is **NOT** leap year <br />
else, it is leap year <br />
* **ex.2**, if real year length is 365.2422, leap year is 366, and normal is 365 <br />
So **normal** output <br />
the output would be 4 25 341 <br />
the 341 meaning if year is divisible by 341, it **IS** leap year <br />
the 25 and the 4 meaning if year is divisible by 4 but **NOT** by 25, it **IS** leap year <br />
else, it is **normal** year <br />
