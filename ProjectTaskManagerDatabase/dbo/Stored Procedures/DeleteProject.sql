CREATE PROCEDURE DeleteProject
    @project_id INT
AS
BEGIN
    DELETE FROM Projects WHERE project_id = @project_id;
END;
