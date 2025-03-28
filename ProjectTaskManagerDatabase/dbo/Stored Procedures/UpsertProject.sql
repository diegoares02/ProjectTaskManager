CREATE PROCEDURE UpsertProject
    @project_id INT = NULL,
    @project_name VARCHAR(255),
    @description TEXT,
    @start_date DATE,
    @end_date DATE
AS
BEGIN
    MERGE Projects AS target
    USING (SELECT @project_id, @project_name, @description, @start_date, @end_date) AS source (project_id, project_name, description, start_date, end_date)
    ON target.project_id = source.project_id
    WHEN MATCHED THEN
        UPDATE SET target.project_name = source.project_name, target.description = source.description, target.start_date = source.start_date, target.end_date = source.end_date, target.updated_at = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (project_name, description, start_date, end_date) VALUES (source.project_name, source.description, source.start_date, source.end_date);
END;
