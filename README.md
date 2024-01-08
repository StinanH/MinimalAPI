# MinimalAPI
----------------------------------------------------------------------------------------
##ER-Schema

![ERdiagram](https://github.com/StinanH/MinimalAPI/assets/146299553/354dc248-d7ec-4104-8600-5e28beb023d3)

----------------------------------------------------------------------------------------
##UML-Diagram

![UML-Diagram (1)](https://github.com/StinanH/MinimalAPI/assets/146299553/6d6e55ca-1846-43c8-bc92-74fe8ed2223b)

----------------------------------------------------------------------------------------

##Hämta alla personer i systemet 
-------------------------------
Get @ "/users"


##Hämta alla intressen som är kopplade till en specifik person
-------------------------------------------------------------
Get @ "/users/{id}/interests"

ex. Get @"/users/1/interests"


##Hämta alla länkar som är kopplade till en specifik person
----------------------------------------------------------
Get @ "/users/{id}/webpages"

ex. Get @ "/users/1/webpages"


##Koppla en person till ett nytt intresse
----------------------------------------
POST @ "/users/{id}/interests"

ex. POST @ "/users/3/interests"
JSON:
{
	"interest" : "Gaming"
}


Lägga in nya länkar för en specifik person och ett specifikt intresse
---------------------------------------------------------------------
POST @ "/users/{id}/webpages"
(användaren måste redan ha en koppling till intresset)

ex. POST @ "/users/2/webpages"
{
	"myInterest" : "Movies",
	"url" : "https://www.netflix.com/se/"
}
