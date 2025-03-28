CREATE PROCEDURE GetProjectTasks
    @project_id INT
AS
BEGIN
    SELECT * from Tasks where project_id = @project_id;
END;
