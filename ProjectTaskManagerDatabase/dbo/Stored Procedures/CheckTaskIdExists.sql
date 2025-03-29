CREATE PROCEDURE CheckTaskIdExists
    @TaskId INT,
    @Exists BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Tasks WHERE task_id = @TaskId)
    BEGIN
        SET @Exists = 1; -- Project ID exists
    END
    ELSE
    BEGIN
        SET @Exists = 0; -- Project ID does not exist
    END
END;
