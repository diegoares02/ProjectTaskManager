CREATE PROCEDURE ReadTask
    @task_id INT
AS
BEGIN
    SELECT task_id, project_id, title, description, due_date, priority, status, assigned_to, created_at, updated_at FROM Tasks WHERE task_id = @task_id;
END;
