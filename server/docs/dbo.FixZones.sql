-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'FixZones' 
)
   DROP PROCEDURE dbo.FixZones
GO

CREATE PROCEDURE dbo.FixZones
AS
	DELETE Zones

	INSERT INTO Zones
	SELECT
		z.id ZoneId,
		z.name [Name],
		CASE z.type
			WHEN 'Arena' THEN 0
			WHEN 'Artifact Acquisition' THEN 1
			WHEN 'Battleground' THEN 2
			WHEN 'Class Hall' THEN 3
			WHEN 'Dungeon' THEN 4
			WHEN 'Raid' THEN 5
			WHEN 'Scenario' THEN 6
			WHEN 'Transit' THEN 7
			WHEN 'Zone' THEN 8
		END AS [Type],
		o.LocationId ParentZoneId,
		r.RegionId
	FROM _zonesraw z
	LEFT JOIN _oldzones o ON z.id = o.ZoneId
	LEFT JOIN Regions r on z.category = r.[Name]
	ORDER BY z.id

GO

GRANT EXECUTE ON dbo.FixZones TO [LAPTOP-01\tmog]

GO