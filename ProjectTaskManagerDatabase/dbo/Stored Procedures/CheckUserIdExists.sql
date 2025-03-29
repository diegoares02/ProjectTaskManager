CREATE PROCEDURE CheckUserIdExists
    @UserId INT,
    @Exists BIT OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE user_id = @UserId)
    BEGIN
        SET @Exists = 1; -- User ID exists
    END
    ELSE
    BEGIN
        SET @Exists = 0; -- User ID does not exist
    END
END;
