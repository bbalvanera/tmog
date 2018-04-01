-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'ClearZonesRaw' 
)
   DROP PROCEDURE dbo.ClearZonesRaw
GO

CREATE PROCEDURE dbo.ClearZonesRaw
AS
	IF OBJECT_ID('dbo.ZonesRaw', 'U') IS NOT NULL
		DELETE FROM _zonesraw

GO

GRANT EXECUTE ON dbo.ClearZonesRaw TO [LAPTOP-01\tmog]

GO