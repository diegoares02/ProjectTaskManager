CREATE PROCEDURE CheckProjectIdExists
    @ProjectId INT,
    @Exists BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Projects WHERE project_id = @ProjectId)
    BEGIN
        SET @Exists = 1; -- Project ID exists
    END
    ELSE
    BEGIN
        SET @Exists = 0; -- Project ID does not exist
    END
END;
