CREATE PROCEDURE UpsertProjectUser
    @project_id INT,
    @user_id INT
AS
BEGIN
    MERGE Project_Users AS target
    USING (SELECT @project_id, @user_id) AS source (project_id, user_id)
    ON target.project_id = source.project_id AND target.user_id = source.user_id
    WHEN NOT MATCHED THEN
        INSERT (project_id, user_id) VALUES (source.project_id, source.user_id);
END;
