CREATE PROCEDURE ReadProjectUsers
    @project_id INT
AS
BEGIN
    SELECT user_id FROM Project_Users WHERE project_id = @project_id;
END;
