CREATE PROCEDURE DeleteProjectUser
    @project_id INT,
    @user_id INT
AS
BEGIN
    DELETE FROM Project_Users WHERE project_id = @project_id AND user_id = @user_id;
END;
