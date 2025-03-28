CREATE PROCEDURE UpsertUser
    @user_id INT = NULL,
    @username VARCHAR(255),
    @email VARCHAR(255),
    @password_hash VARCHAR(255),
    @role_id INT
AS
BEGIN
    MERGE Users AS target
    USING (SELECT @user_id, @username, @email, @password_hash, @role_id) AS source (user_id, username, email, password_hash, role_id)
    ON target.user_id = source.user_id
    WHEN MATCHED THEN
        UPDATE SET target.username = source.username, target.email = source.email, target.password_hash = source.password_hash, target.role_id = source.role_id, target.updated_at = GETDATE()
    WHEN NOT MATCHED THEN
        INSERT (username, email, password_hash, role_id) VALUES (source.username, source.email, source.password_hash, source.role_id);
END;
