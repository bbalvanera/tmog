-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'AllItems' 
)
   DROP PROCEDURE dbo.AllItems
GO

CREATE PROCEDURE dbo.AllItems
	@setId int = NULL,
	@regionId int = NULL,
	@zoneId int = NULL
AS
	SELECT DISTINCT
		c.RegionId,
		c.Name RegionName,
		z.ZoneId,
		z.Name ZoneName,
		IsNull(s.DropLevel, -1) ZoneDifficulty,
		i.SetId,
		i.Name SetName,
		i.Slots SetSlots,
		i.ItemId,
		i.ItemName ItemName,
		i.Quality ItemQuality,
		i.Slot,
		s.WowheadId SourceId,
		s.Description Source,
		s.Type SourceType,
		s.SubType SourceSubType
	FROM
		(SELECT
			s.SetId,
			s.Name,
			s.Slots,
			i.ItemId,
			i.Name ItemName,
			i.Slot,
			i.Quality,
			i.SourceId
		 FROM
			[Sets] s
		 JOIN
			SetItems si ON s.SetId = si.SetId
		 JOIN
			Items i ON si.ItemId = i.ItemId
		) AS i
		LEFT JOIN
			Sources s ON i.SourceId = s.SourceId
		LEFT JOIN
			Zones z ON s.ZoneId = z.ZoneId
		LEFT JOIN
			Locations l ON z.LocationId = l.LocationId
		JOIN
			Regions c ON l.RegionId = c.RegionId
	WHERE
		z.ZoneId IS NOT NULL
		AND SUBSTRING(i.Slots, i.Slot + 1, 1) <> '1'
		AND (
				(i.SetId = @setId OR @setId IS NULL)
				AND (z.ZoneId = @zoneId OR @zoneId IS NULL)
				AND (c.RegionId = @regionId OR @regionId IS NULL)
			)

	ORDER BY
		c.RegionId, z.Name, ZoneDifficulty, SetName, i.SetId, Slot, i.ItemName

GO
