USE PetShopDB 
GO

CREATE PROCEDURE GetFoodForAnimal
    @AnimalId INT
AS

    SELECT FOOD_ID, f.NAME as FOOD_NAME, DESCRIPTION, f.ANIMAL_ID, a.NAME as ANIMAL_NAME, NUMBER_OF_PACKAGES_IN_STORAGE, PACKAGE_WEIGHT_IN_KG, EARLIEST_EXPIRATION_DATE, f.FOOD_KIND_ID, fk.NAME as FOOD_KIND_NAME FROM Food f INNER JOIN Animals a ON f.ANIMAL_ID = a.ANIMAL_ID INNER JOIN FoodKind fk on f.FOOD_KIND_ID = fk.FOOD_KIND_ID
    WHERE f.ANIMAL_ID = @AnimalId ORDER BY PACKAGE_WEIGHT_IN_KG DESC
GO


CREATE PROCEDURE CheckFoodData
AS
   SELECT DISTINCT a.NAME as AnimalName,    
       COUNT(f.FOOD_ID) OVER (PARTITION BY a.ANIMAL_ID) AS NumberOfDifferentFood, 
       MIN(f.PRICE) OVER (PARTITION BY a.ANIMAL_ID) AS MinimumFoodPrice,  
       MAX(f.PRICE) OVER (PARTITION BY a.ANIMAL_ID) AS MaximumFoodPrice,  
       AVG(f.PRICE) OVER (PARTITION BY a.ANIMAL_ID) AS AverageFoodPrice
	FROM Food AS f 
	JOIN Animals AS a  ON f.ANIMAL_ID = a.ANIMAL_ID 	
	JOIN Toys AS t ON t.ANIMAL_ID = a.ANIMAL_ID  
	ORDER BY a.NAME;  
GO


CREATE FUNCTION PackFoodItemsIntoXML
(@animalName VARCHAR(50), 
@foodName VARCHAR(50), 
@description VARCHAR(500),
@price DECIMAL(10,2),
@packageWeight DECIMAL(4,2),
@expDate DATE,
@numOfPackages INT,
@foodKindName VARCHAR(50))
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @FoodItemsXML NVARCHAR(MAX)
	SET @FoodItemsXML = '<food>'+
		CASE WHEN ISNULL(@animalName, '') = '' THEN '' ELSE '<animalid>'+ISNULL(CAST(@animalName  AS VARCHAR(50)), '')+'</animalid>' END + 
		CASE WHEN ISNULL(CAST(@foodName  AS VARCHAR(60)), '') = '' THEN '' ELSE '<foodname>'+ISNULL(CAST(@foodName  AS VARCHAR(50)), '')+'</foodname>' END +
		CASE WHEN ISNULL(CAST(@description  AS VARCHAR(500)), '') = '' THEN '' ELSE '<description>'+ISNULL(CAST(@description  AS VARCHAR(500)), '')+'</description>' END +
		CASE WHEN ISNULL(CAST(@price  AS VARCHAR(50)), '') = '' THEN '' ELSE '<price>'+ISNULL(CAST(@price  AS VARCHAR(50)), '')+'</price>' END +
		CASE WHEN ISNULL(CAST(@packageWeight  AS VARCHAR(10)), '') = '' THEN '' ELSE '<packageweight>'+ISNULL(CAST(@packageWeight  AS VARCHAR(10)), '')+'</packageweight>' END +
		CASE WHEN ISNULL(CAST(@expDate  AS VARCHAR(50)), '') = '' THEN '' ELSE '<expirationdate>'+ISNULL(CAST(@expDate  AS VARCHAR(50)), '')+'</expirationdate>' END +
		CASE WHEN ISNULL(CAST(@numOfPackages  AS VARCHAR(10)), '') = '' THEN '' ELSE '<numberofpackages>'+ISNULL(CAST(@numOfPackages  AS VARCHAR(10)), '')+'</numberofpackages>' END +
		CASE WHEN ISNULL(CAST(@foodKindName  AS VARCHAR(50)), '') = '' THEN '' ELSE '<foodkindname>'+ISNULL(CAST(@foodKindName  AS VARCHAR(50)), '')+'</foodkindname>' END +
	'</food>' 

	RETURN @FoodItemsXML
END
GO


CREATE PROCEDURE [dbo].[FetchFoodItemsXML]
AS

DECLARE @FoodItemsXML NVARCHAR(MAX)
SET @FoodItemsXML=NULL


DECLARE @animalName VARCHAR(50)
DECLARE @foodName VARCHAR(50)
DECLARE @description VARCHAR(500)
DECLARE @price DECIMAL(10,2)
DECLARE @packageWeight DECIMAL(4,2)
DECLARE @expDate DATE
DECLARE @numOfPackages INT
DECLARE @foodKindName VARCHAR(50)


	DECLARE cFood CURSOR LOCAL FORWARD_ONLY STATIC
			FOR
			SELECT 
				a.NAME,
				f.NAME,
				DESCRIPTION,
				PRICE,
				PACKAGE_WEIGHT_IN_KG,
				EARLIEST_EXPIRATION_DATE,
				NUMBER_OF_PACKAGES_IN_STORAGE,
				fk.NAME
			FROM Food f
			JOIN Animals a ON f.ANIMAL_ID = a.ANIMAL_ID
			JOIN FoodKind fk ON f.FOOD_KIND_ID = fk.FOOD_KIND_ID


			OPEN cFood

			WHILE(1=1)
			BEGIN

				SET @animalName = NULL
				SET @foodName = NULL
				SET @description = NULL
				SET @price = NULL
				SET @packageWeight = NULL
				SET @expDate = NULL
				SET @numOfPackages = NULL
				SET @foodKindName = NULL

			  FETCH NEXT FROM cFood INTO
				@animalName,
				@foodName,
				@description,
				@price,
				@packageWeight,
				@expDate,
				@numOfPackages,
				@foodKindName

				IF @@fetch_status <> 0  
					BREAK 	

			SET @FoodItemsXML = ISNULL(@FoodItemsXML,'') + dbo.PackFoodItemsIntoXML(@animalName, @foodName, @description, @price, @packageWeight, @expDate, @numOfPackages, @foodKindName)

			END

		   CLOSE cFood
		   DEALLOCATE cFood

		 SET  @FoodItemsXML = '<allfood>'+ISNULL (@FoodItemsXML, '')+'</allfood>'

	   print @FoodItemsXML	
GO



