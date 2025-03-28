CREATE PROCEDURE ReadUser
    @user_id INT
AS
BEGIN
    SELECT user_id, username, email, role_id, created_at, updated_at FROM Users WHERE user_id = @user_id;
END;
