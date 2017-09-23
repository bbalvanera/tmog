-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'AllItemsByLocation' 
)
   DROP PROCEDURE dbo.AllItemsByLocation
GO

CREATE PROCEDURE dbo.AllItemsByLocation
AS
	SELECT
		l.LocationId LocationId
		,l.Name Location

		,z.ZoneId ZoneId
		,z.Name Zone

		,i.ItemId ItemId
		,i.Name Item
		,i.Slot
		,i.Quality

		,s.[Type] SourceType
		,s.SubType SourceSubType
		,s.WowheadId
		,s.[Description] SourceDescription
		,st.SetId
		,st.Name SetName

	FROM Locations l
		JOIN Zones z ON z.LocationId = l.LocationId
		JOIN Sources s ON s.ZoneId = z.ZoneId
		JOIN Items i ON i.SourceId = s.SourceId
		JOIN SetItems ON i.ItemId = SetItems.ItemId
		JOIN Sets st ON SetItems.SetId = st.SetId
	ORDER BY l.Name
GO