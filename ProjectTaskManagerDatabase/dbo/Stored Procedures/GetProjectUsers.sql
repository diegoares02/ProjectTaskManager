CREATE PROCEDURE GetProjectUsers
    @project_id INT
AS
BEGIN
    SELECT user_id from Project_Users where project_id = @project_id;
END;
