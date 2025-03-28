CREATE PROCEDURE DeleteRole
    @role_id INT
AS
BEGIN
    DELETE FROM Roles WHERE role_id = @role_id;
END;
