CREATE PROCEDURE GetUserTasks
    @user_id INT
AS
BEGIN
    SELECT * from Tasks where assigned_to = @user_id;
END;
