CREATE PROCEDURE GetUserProjects
    @user_id INT
AS
BEGIN
    SELECT project_id from Project_Users WHERE user_id = @user_id;
END;
