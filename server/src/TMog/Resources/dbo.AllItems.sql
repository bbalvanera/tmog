CREATE PROCEDURE dbo.AllItems
    @setId int = NULL,
    @regionId int = NULL,
    @zoneId int = NULL
AS
    SELECT DISTINCT
        ISNULL(r1.RegionId, r2.RegionId) RegionId,
        ISNULL(r1.[Name], r2.[Name]) RegionName,
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
        WHERE SUBSTRING(s.Slots, i.Slot + 1, 1) <> '1'
        ) AS i
        LEFT JOIN
            Sources s ON i.SourceId = s.SourceId
        LEFT JOIN
            Zones z ON s.ZoneId = z.ZoneId
        LEFT JOIN
            Regions r1 ON z.RegionId = r1.RegionId
        LEFT JOIN
            Zones l ON z.ParentZoneId = l.ZoneId
        LEFT JOIN
            Regions r2 ON l.RegionId = r2.RegionId
    WHERE
        z.ZoneId IS NOT NULL
        AND s.[Type] = 2 --DROP
        AND (
                (i.SetId = @setId OR @setId IS NULL)
                AND (z.ZoneId = @zoneId OR @zoneId IS NULL)
                AND ((r1.RegionId = @regionId OR r2.RegionID = @regionId OR @regionId IS NULL))
            )

    ORDER BY
        RegionId, z.Name, ZoneDifficulty, SetName, i.SetId, Slot, i.ItemName

; GRANT EXECUTE ON dbo.Allitems TO [LAPTOP-01\tmog]
