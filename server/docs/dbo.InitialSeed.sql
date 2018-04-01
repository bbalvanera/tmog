INSERT INTO Regions SELECT * FROM TMogSeed..Regions
INSERT INTO Zones SELECT * FROM TMogSeed..Zones
INSERT INTO	Factions SELECT * FROM TMogSeed..Factions

SET IDENTITY_INSERT Sources ON
INSERT INTO Sources (SourceId, [Type], [Description], SubType, DropLevel, WowheadId, ZoneId)
SELECT SourceId, [Type], [Description], SubType, DropLevel, WowheadId, ZoneId FROM TMogSeed..Sources
SET IDENTITY_INSERT Sources OFF

INSERT INTO Items SELECT * FROM TMogSeed..Items
INSERT INTO [Sets] SELECT * FROM TMogSeed..[Sets]
INSERT INTO SetItems SELECT * FROM TMogSeed..SetItems
