CREATE PROCEDURE CheckTaskTitleExists
    @Title VARCHAR(255),
    @Exists BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Tasks WHERE title = @Title)
    BEGIN
        SET @Exists = 1; -- Title exists
    END
    ELSE
    BEGIN
        SET @Exists = 0; -- Title does not exist
    END
END;
