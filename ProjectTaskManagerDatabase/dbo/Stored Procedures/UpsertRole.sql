CREATE PROCEDURE UpsertRole
    @role_id INT = NULL,
    @role_name VARCHAR(50),
    @description TEXT
AS
BEGIN
    MERGE Roles AS target
    USING (SELECT @role_id, @role_name, @description) AS source (role_id, role_name, description)
    ON target.role_id = source.role_id
    WHEN MATCHED THEN
        UPDATE SET target.role_name = source.role_name, target.description = source.description
    WHEN NOT MATCHED THEN
        INSERT (role_name, description) VALUES (source.role_name, source.description);
END;
