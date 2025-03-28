CREATE PROCEDURE ReadProject
    @project_id INT
AS
BEGIN
    SELECT project_id, project_name, description, start_date, end_date, created_at, updated_at FROM Projects WHERE project_id = @project_id;
END;
