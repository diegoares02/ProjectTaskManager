CREATE PROCEDURE UpsertTask
    @task_id INT = NULL,
    @project_id INT,
    @title VARCHAR(255),
    @description TEXT,
    @due_date DATE,
    @priority VARCHAR(10),
    @status VARCHAR(15),
    @assigned_to INT
AS
BEGIN
    MERGE Tasks AS target
    USING (SELECT @task_id, @project_id, @title, @description, @due_date, @priority, @status, @assigned_to) AS source (task_id, project_id, title, description, due_date, priority, status, assigned_to)
    ON target.task_id = source.task_id
    WHEN MATCHED THEN
        UPDATE SET target.project_id = source.project_id, target.title = source.title, target.description = source.description, target.due_date = source.due_date, target.priority = source.priority, target.status = source.status, target.assigned_to = source.assigned_to, target.updated_at = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (project_id, title, description, due_date, priority, status, assigned_to) VALUES (source.project_id, source.title, source.description, source.due_date, source.priority, source.status, source.assigned_to);
END;
