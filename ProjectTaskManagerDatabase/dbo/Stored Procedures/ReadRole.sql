CREATE PROCEDURE ReadRole
    @role_id INT
AS
BEGIN
    SELECT role_id, role_name, description FROM Roles WHERE role_id = @role_id;
END;
